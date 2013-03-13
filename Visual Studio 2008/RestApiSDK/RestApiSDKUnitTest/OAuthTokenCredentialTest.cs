using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PayPal;
using PayPal.Manager;

namespace RestApiSDKUnitTest
{   
    /// <summary>
    ///This is a test class for OAuthTokenCredentialTest and is intended
    ///to contain all OAuthTokenCredentialTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OAuthTokenCredentialTest
    {
        /// <summary>
        /// Gets the client ID key, issued to you by PayPal when you created the account.
        /// </summary>
        /// <value>
        /// The client ID key as string.
        /// </value>
        private string ClientID
        {
            get
            {
                string clntID = ConfigManager.Instance.GetProperty("ClientID");
                return clntID;
            }
        }

        /// <summary>
        /// Gets the client secret key issued to you by PayPal when you created the account.
        /// </summary>
        /// <value>
        /// The client secret key as string.
        /// </value>
        private string ClientSecret
        {
            get
            {
                string clntSecret = ConfigManager.Instance.GetProperty("ClientSecret");
                return clntSecret;
            }
        }

        /// <summary>
        /// Gets the access token issued by PayPal, That is ssigned in response. 
        /// The access token expires 8 hours after it is issued, after which you’ll have to request a new access token. 
        /// </summary>
        /// <value>
        /// The access token issued by PayPal.
        /// </value>
        private string AccessToken
        {
            get
            {
                string tokenAccess = new OAuthTokenCredential(ClientID, ClientSecret).GetAccessToken();
                return tokenAccess;
            }
        }

        /// <summary>
        /// Generates the client id in base64.
        /// </summary>
        /// <returns>The client id in base64.</returns>
        private string GenerateClientIdInBase64()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(ClientID);
            string base64ClientID = Convert.ToBase64String(bytes);
            return base64ClientID;
        }

        /// <summary>
        /// Generates the oAuth token.
        /// </summary>
        /// <returns>the oAuth token</returns>
        private string GenerateOAuthToken()
        {
            string response = null;
            string base64ClientID = GenerateClientIdInBase64();
            string url = ConfigManager.Instance.GetProperty("oauth.EndPoint");
            ConnectionManager connManager = ConnectionManager.Instance;
            HttpWebRequest httpRequest = connManager.GetConnection(url);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Basic " + base64ClientID);
            string postRequest = "grant_type=client_credentials&scope=https://api.paypal.com/services/payments/dcc&response_type=token";
            httpRequest.Method = "POST";
            httpRequest.Accept = "*/*";
            foreach (KeyValuePair<string, string> header in headers)
            {
                httpRequest.Headers.Add(header.Key, header.Value);
            }

            using (StreamWriter writerStream = new StreamWriter(httpRequest.GetRequestStream()))
            {
                writerStream.Write(postRequest);
            }
            using (WebResponse responseWeb = httpRequest.GetResponse())
            {
                using (StreamReader readerStream = new StreamReader(responseWeb.GetResponseStream()))
                {
                    response = readerStream.ReadToEnd();
                }
            }
            JObject deserializedObject = (JObject)JsonConvert.DeserializeObject(response);
            string generatedToken = (string)deserializedObject["token_type"] + " " + (string)deserializedObject["access_token"];
            return generatedToken;
        }

        private string GenerateAccessToken()
        {
            string generatedToken = null;
            string base64ClientID = GenerateClientIdInBase64();
            generatedToken = GenerateOAuthToken();
            return generatedToken;
        }

        [TestMethod()]
        public void OAuthTokenCredentialConstructorTest()
        {
            OAuthTokenCredential target = new OAuthTokenCredential(ClientID, ClientSecret);
            Assert.IsNotNull(target);
        }

        [TestMethod()]
        public void GetAccessTokenTest()
        {
            OAuthTokenCredential target = new OAuthTokenCredential(ClientID, ClientSecret); // TODO: Initialize to an appropriate value
            string expected = target.GetAccessToken();
            string actual = target.GetAccessToken();
            Assert.IsTrue(actual.ToLower().Contains("bearer"));
        }
    }
}
