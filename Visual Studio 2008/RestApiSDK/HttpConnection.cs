using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
// NuGet Install
// install PayPalCoreSDK -excludeversion -outputDirectory .\Packages
// 2.0
using log4net;
using PayPal.Exception;
using PayPal.Manager;

namespace PayPal
{
    /// <summary>
    /// Http Connection Object
    /// </summary>
    public class HttpConnection
    {
        /// <summary>
        /// PayPal exception Logger
        /// </summary>
        private static ILog logger = LogManagerWrapper.GetLogger(typeof(ConnectionManager));

        /// <summary>
        /// The List of retry codes
        /// </summary>
        private static ArrayList retryCodes = new ArrayList(new HttpStatusCode[] 
                                                { HttpStatusCode.GatewayTimeout,
                                                  HttpStatusCode.RequestTimeout,
                                                  HttpStatusCode.InternalServerError,
                                                  HttpStatusCode.ServiceUnavailable,
                                                });

        /// <summary>
        /// Executes the specified pay load.
        /// </summary>
        /// <param name="payLoad">The pay load.</param>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns></returns>
        /// <exception cref="PayPal.Exception.ConnectionException">Invalid HTTP response  + ex.Message</exception>
        /// <exception cref="PayPalException">
        /// Exception in HttpConnection Execute:  + ex.Message
        /// or
        /// Exception in HttpConnection Execute
        /// </exception>
        public string Execute(string payLoad, HttpWebRequest httpRequest)
        {
            try
            {
                switch (httpRequest.Method)
                {
                    case "POST":
                        using (StreamWriter writerStream = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            if (!string.IsNullOrEmpty(payLoad))
                            {
                                writerStream.Write(payLoad);
                                writerStream.Flush();
                                writerStream.Close();
                                logger.Debug(payLoad);
                            }

                        }
                        break;
                    default:
                        break;
                }

                ConfigManager configMngr = ConfigManager.Instance;
                int retriesConfigured = (configMngr.GetProperty(BaseConstants.HTTP_CONNECTION_RETRY) != null) ?
                    Convert.ToInt32(configMngr.GetProperty(BaseConstants.HTTP_CONNECTION_RETRY)) : 0;
                int retries = 0;

                do
                {
                    try
                    {                        
                        using (WebResponse responseWeb = httpRequest.GetResponse())
                        {
                            using (StreamReader readerStream = new StreamReader(responseWeb.GetResponseStream()))
                            {
                                string response = readerStream.ReadToEnd().Trim();
                                logger.Debug("Service response");
                                logger.Debug(response);
                                return response;
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        if (ex.Response is HttpWebResponse)
                        {
                            HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                            logger.Info("Got " + statusCode.ToString() + " response from server");
                        }
                        if (!RequiresRetry(ex))
                        {
                            // Server responses in the range of 4xx and 5xx throw a WebException
                            throw new ConnectionException("Invalid HTTP response " + ex.Message);
                        }                       
                    }

                } while (retries++ < retriesConfigured);
            }
            catch (System.Exception ex)
            {
                throw new PayPalException("Exception in HttpConnection Execute: " + ex.Message, ex);
            }
            throw new PayPalException("Exception in HttpConnection Execute");
        }

        /// <summary>
        /// Returns true if a HTTP retry is required
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static bool RequiresRetry(WebException ex)
        {
            if (ex.Status != WebExceptionStatus.ProtocolError)
            {
                return false;
            }
            HttpStatusCode status = ((HttpWebResponse)ex.Response).StatusCode;
            return retryCodes.Contains(status);
        }
    }
}
