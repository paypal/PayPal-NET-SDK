using System;
using System.Collections.Generic;
using System.Text;
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
// install Newtonsoft.Json -excludeversion -outputDirectory .\Packages
// net35
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PayPal
{
    /// <summary>
    /// Retrieve the access token from OAuthTokenCredential by passing in ClientID and ClientSecret.
    /// </summary>
    public class OAuthTokenCredential
    {

        /// <summary>
        /// oAUTH Token Path
        /// </summary>
        private const string OAUTHTOKENPATH = "/v1/oauth2/token";
        /// <summary>
        /// PayPal Client ID for OAuth
        /// </summary>
        private String clientID;

        /// <summary>
        /// PayPal Client Secret for OAuth
        /// </summary>
        private string clientSecret;

        /// <summary>
        /// Access Token that is generated
        /// </summary>
        private string accessToken;

        /// <summary>
        /// Application ID returned by OAuth servers
        /// </summary>
        private string appID;

        /// <summary>
        /// Seconds for with access token is valid
        /// </summary>
        private int secondsToExpire;

        /// <summary>
        /// Last time when access token was generated
        /// </summary>
        private long timeInMilliseconds;

        /// <summary>
        /// Logs output statements, errors, debug info to a text file    
        /// </summary>
        private static readonly ILog logger = LogManagerWrapper.GetLogger(typeof(OAuthTokenCredential));
               
        /// <summary>
        /// Client ID and Secret for the OAuth
        /// </summary>
        /// <param name="clientID">The PayPal Client ID for OAuth</param>
        /// <param name="clientSecret">The PayPal Client Secret string for OAuth</param>
        public OAuthTokenCredential(String clientID, String clientSecret)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Gets the oAuth access token.
        /// </summary>
        /// <returns>The oAuth access token</returns>
        public string GetAccessToken()
        {
            // If Access Token is not Null and time has lapsed
            if (accessToken != null)
            {
                // If the token has not expired
                // Set TTL as expiresTime - 60000
                // If expired set accesstoken == null
                if (((DateTime.Now.Millisecond - timeInMilliseconds) / 1000) > (secondsToExpire - 120))
                {
                    // regenerate token
                    accessToken = null;
                }
            }
            // If accessToken is Null, Compute it
            if (accessToken == null)
            {
                // Write Logic for passing in Detail to Identity Api Serv and
                // computing the token
                // Set the Value inside the accessToken and result
                accessToken = GenerateAccessToken();
            }
            return accessToken;
        }

        /// <summary>
        /// Generates the access token.
        /// </summary>
        /// <returns>The generated oAuth access token as string</returns>
        private string GenerateAccessToken()
        {
            string generatedToken = null;
            string base64ClientID = GenerateBase64String(clientID + ":" + clientSecret);
            generatedToken = GenerateOAuthToken(base64ClientID);
            return generatedToken;
        }

        /// <summary>
        /// Generates the base64 string.
        /// </summary>
        /// <param name="clientCredential">The client credential.</param>
        /// <returns>
        /// The base64 string
        /// </returns>
        /// <exception cref="PayPalException">
        /// </exception>
        private string GenerateBase64String(string clientCredential)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(clientCredential);
                string base64ClientID = Convert.ToBase64String(bytes);
                return base64ClientID;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
            catch (NotSupportedException ex)
            {
                throw new PayPalException(ex.Message, ex);
            }
            catch (System.Exception ex)
            {
                throw new PayPalException(ex.Message, ex);
            }           
        }

        private string GenerateOAuthToken(string base64ClientID)
        { 
            try
            {
                string response = null;
                
                Uri uniformResourceIdentifier = null;
                Uri baseUri = null;
                if (ConfigManager.Instance.GetProperty("oauth.EndPoint")!=null)
                {
                    baseUri = new Uri(ConfigManager.Instance.GetProperty("oauth.EndPoint"));
                }
                else
                {
                    baseUri = new Uri(ConfigManager.Instance.GetProperty("endpoint"));
                }
                bool success = Uri.TryCreate(baseUri, OAUTHTOKENPATH, out uniformResourceIdentifier);


                ConnectionManager connManager = ConnectionManager.Instance;
                HttpWebRequest httpRequest = connManager.GetConnection(uniformResourceIdentifier.AbsoluteUri);  
              
                
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Basic " + base64ClientID);
                
                string postRequest = "grant_type=client_credentials";
                httpRequest.Method = "POST";
                httpRequest.Accept = "*/*";
                
                foreach (KeyValuePair<string, string> header in headers)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }

                using (StreamWriter writerStream = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    writerStream.Write(postRequest);
                    logger.Debug(postRequest);
                }
                using (WebResponse responseWeb = httpRequest.GetResponse())
                {
                    using (StreamReader readerStream = new StreamReader(responseWeb.GetResponseStream()))
                    {
                        response = readerStream.ReadToEnd();
                        logger.Debug("Service response");
                        logger.Debug(response);
                    }
                }
                JObject deserializedObject = (JObject)JsonConvert.DeserializeObject(response);
                string generatedToken = (string)deserializedObject["token_type"] + " " + (string)deserializedObject["access_token"];
                appID = (string)deserializedObject["app_id"];
                secondsToExpire = (int)deserializedObject["expires_in"];
                timeInMilliseconds = DateTime.Now.Millisecond;
                return generatedToken;
            }            
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    logger.Info("Got " + statusCode.ToString() + " response from server");
                }
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
    }
}
