using PayPal.Api;
using PayPal.Log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace PayPal
{
    /// <summary>
    /// Manager class for storing X509 certificates.
    /// </summary>
    public sealed class CertificateManager
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static Logger logger = Logger.GetLogger(typeof(CertificateManager));

        /// <summary>
        /// Cache of X509 certificates.
        /// </summary>
        private static ConcurrentDictionary<string, X509Certificate2Collection> certificates;

        /// <summary>
        /// Private constructor prevent direct instantiation
        /// </summary>
        private CertificateManager()
        {
            certificates = new ConcurrentDictionary<string, X509Certificate2Collection>();
        }

        /// <summary>
        /// Private static member for storing the single instance.
        /// </summary>
        private static volatile CertificateManager instance;

        /// <summary>
        /// Private static member for locking the singleton object while it's being instantiated.
        /// </summary>
        private static object syncRoot = new Object();

        /// <summary>
        /// Gets the singleton instance for this class.
        /// </summary>
        public static CertificateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CertificateManager();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the certificate corresponding to the specified URL from the cache of certificates.  If the cache doesn't contain the certificate, it is downloaded and verified.
        /// </summary>
        /// <param name="certUrl">The URL pointing to the certificate.</param>
        /// <returns>An <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2"/> object containing the details of the certificate.</returns>
        /// <exception cref="PayPal.PayPalException">Thrown if the downloaded certificate cannot be verified.</exception>
        public X509Certificate2Collection GetCertificatesFromUrl(string certUrl)
        {
            // If we haven't already cached this URL, then download, verify, and cache it.
            if(!certificates.ContainsKey(certUrl))
            {
                // Download the certificate.
                string certData;
                using (var webClient = new WebClient())
                {
                    certData = webClient.DownloadString(certUrl);
                }

                // Load all the certificates.
                // NOTE: The X509Certificate2Collection.Import() method only
                // imports the first certifcate, even if a stream contains
                // multiple certificates. For this reason, we'll load the
                // certificates one-by-one, verifying as we go.
                var results = certData.Split(new string[] { "-----BEGIN CERTIFICATE-----", "-----END CERTIFICATE-----" }, StringSplitOptions.RemoveEmptyEntries);
                var collection = new X509Certificate2Collection();
                foreach (var result in results)
                {
                    var trimmed = result.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        var certificate = new X509Certificate2(System.Text.Encoding.UTF8.GetBytes(trimmed));

                        // Verify the certificate before adding it to the collection.
                        if(certificate.Verify())
                        {
                            collection.Add(certificate);
                        }
                        else
                        {
                            throw new PayPalException("Unable to verify the certificate(s) found at " + certUrl);
                        }
                    }
                }

                certificates[certUrl] = collection;
            }

            return certificates[certUrl];
        }

        /// <summary>
        /// Gets the trusted certificate to be used in validating a certificate chain.
        /// </summary>
        /// <param name="config">Config containing an optional path to the trusted certificate file to use.</param>
        /// <returns>An <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2"/> object containing the trusted certificate to use in validating a certificate chain.</returns>
        public X509Certificate2 GetTrustedCertificateFromFile(Dictionary<string, string> config)
        {
            try
            {
                if (config != null && config.ContainsKey(BaseConstants.TrustedCertificateLocation))
                {
                    return new X509Certificate2(File.ReadAllBytes(config[BaseConstants.TrustedCertificateLocation]));
                }

                using (var reader = Assembly.GetExecutingAssembly().GetManifestResourceStream("PayPal.Resources.DigiCertSHA2ExtendedValidationServerCA.crt"))
                using (var memoryStream = new MemoryStream())
                {
                    reader.CopyTo(memoryStream);
                    return new X509Certificate2(memoryStream.ToArray());
                }
            }
            catch(Exception ex)
            {
                throw new PayPalException("Unable to load trusted certificate.", ex);
            }
        }

        /// <summary>
        /// Validates the certificate chain for the specified client certificate using a known, trusted certificate.
        /// </summary>
        /// <param name="trustedCert">Trusted certificate to use in validating the chain.</param>
        /// <param name="clientCerts">Client certificates to use in validating the chain.</param>
        /// <returns>True if the certificate chain is valid; false otherwise.</returns>
        public bool ValidateCertificateChain(X509Certificate2 trustedCert, X509Certificate2Collection clientCerts)
        {
            // If there's no trusted or client certificates provided, return immediately.
            if (trustedCert == null || clientCerts == null || clientCerts.Count <= 0)
            {
                return false;
            }

            var chain = new X509Chain();
            chain.ChainPolicy.ExtraStore.AddRange(clientCerts);
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;

            // Attempt to build the complete certificate chain using the first certificate.
            if (!chain.Build(clientCerts[0]))
            {
                return false;
            }

            // Verify the chain not only includes verified certificates, but
            // also includes a match to the provided trusted certificate.
            foreach(var chainElement in chain.ChainElements)
            {
                if(!chainElement.Certificate.Verify())
                {
                    return false;
                }

                if(chainElement.Certificate.Thumbprint == trustedCert.Thumbprint)
                {
                    // The trusted certificate has been found, so we don't need
                    // to venture any further up the chain.
                    return ValidatePayPalClientCertificate(clientCerts);
                }
            }

            // If we've gotten this far, it means we were unable to detect the
            // provided trusted certificate in the certificate chain from the
            // provided client certificate.
            return false;
        }

        /// <summary>
        /// Validates the leaf client cert for the owner to be PayPal
        /// </summary>
        /// <param name="clientCerts"></param>
        /// <returns>True if leaf client certificate belongs to .paypal.com, false otherwise</returns>
        public bool ValidatePayPalClientCertificate(X509Certificate2Collection clientCerts)
        {
            // If there's no client certificates provided, return immediately.
            if (clientCerts == null || clientCerts.Count <= 0)
            {
                return false;
            }

            String subjectName = clientCerts[0].Subject;
            String[] results = Regex.Matches(subjectName, @"CN=[a-zA-Z._-]+").Cast<Match>().Select(m => m.Value).ToArray();
            return (results != null && results.Length > 0 && results[0].StartsWith("CN=") && results[0].EndsWith(".paypal.com"));
        }
    }
}
