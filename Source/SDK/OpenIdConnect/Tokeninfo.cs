using System;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using PayPal.Util;

namespace PayPal.Api
{
    /// <summary>
    /// Class that stores OpenIdConnect access token information.
    /// </summary>
    public class Tokeninfo : PayPalResource
    {
        /// <summary>
        /// OPTIONAL, if identical to the scope requested by the client otherwise, REQUIRED
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string scope { get; set; }

        /// <summary>
        /// The access token issued by the authorization server
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string access_token { get; set; }

        /// <summary>
        /// The refresh token, which can be used to obtain new access tokens using the same authorization grant as described in OAuth2.0 RFC6749 in Section 6
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string refresh_token { get; set; }

        /// <summary>
        /// The type of the token issued as described in OAuth2.0 RFC6749 (Section 7.1), value is case insensitive
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string token_type { get; set; }

        /// <summary>
        /// The lifetime in seconds of the access token
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int expires_in { get; set; }

        /// <summary>
        /// Explicit default constructor
        /// </summary>
        public Tokeninfo() { }

        /// <summary>
        /// Constructor overload
        /// </summary>
        public Tokeninfo(string accessToken, string tokenType, int expiresIn)
        {
            this.access_token = accessToken;
            this.token_type = tokenType;
            this.expires_in = expiresIn;
        }

        /// <summary>
        /// Creates an Access Token from an Authorization Code.
        /// <param name="createFromAuthorizationCodeParameters">Query parameters used for API call</param>
        /// </summary>
        public static Tokeninfo CreateFromAuthorizationCode(CreateFromAuthorizationCodeParameters createFromAuthorizationCodeParameters)
        {
            return CreateFromAuthorizationCode(null, createFromAuthorizationCodeParameters);
        }

        /// <summary>
        /// Creates an Access Token from an Authorization Code.
        /// <param name="apiContext">APIContext to be used for the call.</param>
        /// <param name="createFromAuthorizationCodeParameters">Query parameters used for API call</param>
        /// </summary>
        public static Tokeninfo CreateFromAuthorizationCode(APIContext apiContext, CreateFromAuthorizationCodeParameters createFromAuthorizationCodeParameters)
        {
            var pattern = "v1/identity/openidconnect/tokenservice?grant_type={0}&code={1}&redirect_uri={2}";
            var parameters = new object[] { createFromAuthorizationCodeParameters };
            var resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
            return CreateFromAuthorizationCodeParameters(apiContext, createFromAuthorizationCodeParameters, resourcePath);
        }

        /// <summary>
        /// Creates Access and Refresh Tokens from an Authorization Code for future payments.
        /// </summary>
        /// <param name="apiContext">APIContext to be used for the call.</param>
        /// <param name="createFromAuthorizationCodeParameters">Query parameters used for the API call.</param>
        /// <returns>A TokenInfo object containing the Access and Refresh Tokens.</returns>
        public static Tokeninfo CreateFromAuthorizationCodeForFuturePayments(APIContext apiContext, CreateFromAuthorizationCodeParameters createFromAuthorizationCodeParameters)
        {
            var pattern = "v1/oauth2/token?grant_type=authorization_code&response_type=token&redirect_uri=urn:ietf:wg:oauth:2.0:oob&code={0}";
            var parameters = new object[] { createFromAuthorizationCodeParameters.ContainerMap["code"] };
            var resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
            return CreateFromAuthorizationCodeParameters(apiContext, createFromAuthorizationCodeParameters, resourcePath);
        }

        /// <summary>
        /// Helper method for creating Access and Refresh Tokens from an Authorization Code.
        /// </summary>
        /// <param name="apiContext">APIContext to be used for the call.</param>
        /// <param name="createFromAuthorizationCodeParameters">Query parameters used for the API call.</param>
        /// <param name="resourcePath">The path to the REST API resource that will be requested.</param>
        /// <returns>A TokenInfo object containing the Access and Refresh Tokens.</returns>
        private static Tokeninfo CreateFromAuthorizationCodeParameters(APIContext apiContext, CreateFromAuthorizationCodeParameters createFromAuthorizationCodeParameters, string resourcePath)
        {
            var payLoad = resourcePath.Substring(resourcePath.IndexOf('?') + 1);
            resourcePath = resourcePath.Substring(0, resourcePath.IndexOf("?"));
            var headersMap = new Dictionary<string, string>();
            headersMap.Add(BaseConstants.ContentTypeHeader, "application/x-www-form-urlencoded");
            if (apiContext == null)
            {
                apiContext = new APIContext();
            }
            apiContext.HTTPHeaders = headersMap;
            apiContext.MaskRequestId = true;
            return PayPalResource.ConfigureAndExecute<Tokeninfo>(apiContext, HttpMethod.POST, resourcePath, payLoad);
        }

        /// <summary>
        /// Creates an Access Token from an Refresh Token.
        /// <param name="createFromRefreshTokenParameters">Query parameters used for API call</param>
        /// </summary>
        public Tokeninfo CreateFromRefreshToken(CreateFromRefreshTokenParameters createFromRefreshTokenParameters)
        {
            return CreateFromRefreshToken(null, createFromRefreshTokenParameters);
        }

        /// <summary>
        /// Creates an Access Token from an Refresh Token
        /// <param name="apiContext">APIContext to be used for the call</param>
        /// <param name="createFromRefreshTokenParameters">Query parameters used for API call</param>
        /// </summary>
        public Tokeninfo CreateFromRefreshToken(APIContext apiContext, CreateFromRefreshTokenParameters createFromRefreshTokenParameters)
        {
            if(!createFromRefreshTokenParameters.ContainerMap.ContainsKey("client_id"))
            {
                createFromRefreshTokenParameters.ContainerMap["client_id"] = apiContext.Config[BaseConstants.ClientId];
            }
            if (!createFromRefreshTokenParameters.ContainerMap.ContainsKey("client_secret"))
            {
                createFromRefreshTokenParameters.ContainerMap["client_secret"] = apiContext.Config[BaseConstants.ClientSecret];
            }
            string pattern = "v1/identity/openidconnect/tokenservice?grant_type={0}&refresh_token={1}&scope={2}&client_id={3}&client_secret={4}";
            createFromRefreshTokenParameters.SetRefreshToken(HttpUtility.UrlEncode(refresh_token));
            object[] parameters = new object[] { createFromRefreshTokenParameters };
            string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
            string payLoad = resourcePath.Substring(resourcePath.IndexOf('?') + 1);
            resourcePath = resourcePath.Substring(0, resourcePath.IndexOf("?"));
            Dictionary<string, string> headersMap = new Dictionary<string, string>();
            headersMap.Add(BaseConstants.ContentTypeHeader, "application/x-www-form-urlencoded");
            if (apiContext == null)
            {
                apiContext = new APIContext();
            }
            apiContext.HTTPHeaders = headersMap;
            apiContext.MaskRequestId = true;

            // Set the authentication header
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", apiContext.Config[BaseConstants.ClientId], apiContext.Config[BaseConstants.ClientSecret]));
            apiContext.AccessToken = "Basic " + Convert.ToBase64String(bytes);

            return PayPalResource.ConfigureAndExecute<Tokeninfo>(apiContext, HttpMethod.POST, resourcePath, payLoad);
        }
    }
}



