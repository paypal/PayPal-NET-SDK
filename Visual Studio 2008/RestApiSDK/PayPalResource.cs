using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
// NuGet Install
// install PayPalCoreSDK -excludeversion -outputDirectory .\Packages
// 2.0
using log4net;
using PayPal.Exception;
// install Newtonsoft.Json -excludeversion -outputDirectory .\Packages
// net35
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Manager;

namespace PayPal
{
    /// <summary>
    /// PayPal Resources
    /// </summary>
    public abstract class PayPalResource
    {
        /// <summary>
        /// Logs output statements, errors, debug info to a text file
        /// </summary>
        private static readonly ILog logger = LogManagerWrapper.GetLogger(typeof(PayPalResource));

        /// <summary>
        /// The list of possible retry codes
        /// </summary>
        private static ArrayList retryCodes = new ArrayList(new HttpStatusCode[] 
                                                { HttpStatusCode.GatewayTimeout,
                                                  HttpStatusCode.RequestTimeout,
                                                  HttpStatusCode.InternalServerError,
                                                  HttpStatusCode.ServiceUnavailable,
                                                });

        public const string SdkID = "ID";

        public const string SdkVersion = "1.0";

        public static T ConfigureAndExecute<T>(string accessToken, HttpMethod httpMethod, string resource, string payLoad)
        {
            try
            {
                string response = null;
                Dictionary<string, String> headers;
                Uri uniformResourceIdentifier = null;
                Uri baseUri = null;

                baseUri = new Uri(ConfigManager.Instance.GetProperty("endpoint"));
                bool success = Uri.TryCreate(baseUri, resource, out uniformResourceIdentifier);

                RESTConfiguration restConfiguration = new RESTConfiguration();
                restConfiguration.authorizationToken = accessToken;
                headers = restConfiguration.GetHeaders();
         
                ConnectionManager connMngr = ConnectionManager.Instance;
                connMngr.GetConnection(uniformResourceIdentifier.ToString());
                HttpWebRequest httpRequest = connMngr.GetConnection(uniformResourceIdentifier.ToString());
                httpRequest.Method = httpMethod.ToString();
                httpRequest.ContentType = "application/json";
                httpRequest.ContentLength = payLoad.Length;               

                foreach (KeyValuePair<string, string> header in headers)
                {
                    if (header.Key.Trim().Equals("User-Agent"))
                    {
                        httpRequest.UserAgent = header.Value;
                    }                   
                    else
                    {
                        httpRequest.Headers.Add(header.Key, header.Value);
                    }
                }
                if (logger.IsDebugEnabled)
                {
                    foreach (string headerName in httpRequest.Headers)
                    {
                        logger.Debug(headerName + ":" + httpRequest.Headers[headerName]);
                    }
                }
                HttpConnection connectionHttp = new HttpConnection();
                response = connectionHttp.Execute(payLoad, httpRequest);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (UriFormatException ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
            catch (IOException ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
            catch (System.Exception ex)
            {
                throw new PayPalException(ex.Message, ex);
            }           
        }

        public static T ConfigureAndExecute<T>(APIContext apiContext, HttpMethod httpMethod, string resource, string payLoad)
        {
            try
            {
                string response = null;
                Dictionary<string, String> headers;
                Uri uniformResourceIdentifier = null;
                Uri baseUri = null;

                baseUri = new Uri(ConfigManager.Instance.GetProperty("endpoint"));
                bool success = Uri.TryCreate(baseUri, resource, out uniformResourceIdentifier);

                RESTConfiguration restConfiguration = new RESTConfiguration();
                restConfiguration.authorizationToken = apiContext.AccessToken;
                restConfiguration.requestId = apiContext.RequestID;
                headers = restConfiguration.GetHeaders();

                ConnectionManager connMngr = ConnectionManager.Instance;
                connMngr.GetConnection(uniformResourceIdentifier.ToString());
                HttpWebRequest httpRequest = connMngr.GetConnection(uniformResourceIdentifier.ToString());
                httpRequest.Method = httpMethod.ToString();
                httpRequest.ContentType = "application/json";
                httpRequest.ContentLength = payLoad.Length;

                foreach (KeyValuePair<string, string> header in headers)
                {
                    if (header.Key.Trim().Equals("User-Agent"))
                    {
                        httpRequest.UserAgent = header.Value;
                    }
                    else
                    {
                        httpRequest.Headers.Add(header.Key, header.Value);
                    }
                }
                if (logger.IsDebugEnabled)
                {
                    foreach (string headerName in httpRequest.Headers)
                    {
                        logger.Debug(headerName + ":" + httpRequest.Headers[headerName]);
                    }
                }
                HttpConnection connectionHttp = new HttpConnection();
                response = connectionHttp.Execute(payLoad, httpRequest);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (UriFormatException ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
            catch (IOException ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
            catch (System.Exception ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
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
