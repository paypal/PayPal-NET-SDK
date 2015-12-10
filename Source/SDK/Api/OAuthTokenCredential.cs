using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayPal.Log;

namespace PayPal.Api
{
    /// <summary>
    /// OAuthTokenCredential is used for generation of OAuth Token used by PayPal
    /// REST API service. clientId and clientSecret are required by the class to
    /// generate OAuth Token, the resulting token is of the form "Bearer xxxxxx". The
    /// class has two constructors, one of it taking an additional Dictionary
    /// used for dynamic configuration.
    /// </summary>
    public class OAuthTokenCredential
    {
        /// <summary>
        /// Specifies the PayPal endpoint for sending an OAuth request.
        /// </summary>
        private const string OAuthTokenPath = "/v1/oauth2/token";

        /// <summary>
        /// Dynamic configuration map
        /// </summary>
        private Dictionary<string, string> config;

        /// <summary>
        /// Cached access token that is generated when calling <see cref="OAuthTokenCredential.GetAccessToken()"/>.
        /// </summary>
        private string accessToken;

        /// <summary>
        /// SDKVersion instance
        /// </summary>
        private SDKVersion SdkVersion;

        /// <summary>
        /// Gets the client ID to be used when creating an OAuth token.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Gets the client secret to be used when creating an OAuth token.
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Gets the application ID returned by OAuth servers.
        /// Must first call <see cref="OAuthTokenCredential.GetAccessToken()"/> to populate this property.
        /// </summary>
        public string ApplicationId { get; private set; }

        /// <summary>
        /// Gets or sets the lifetime of a created access token in seconds.
        /// Must first call <see cref="OAuthTokenCredential.GetAccessToken()"/> to populate this property.
        /// </summary>
        public int AccessTokenExpirationInSeconds { get; set; }

        /// <summary>
        /// Gets the last date when access token was generated.
        /// Must first call <see cref="OAuthTokenCredential.GetAccessToken()"/> to populate this property.
        /// </summary>
        public DateTime AccessTokenLastCreationDate { get; private set; }

        /// <summary>
        /// Gets or sets the safety gap when checking the expiration of an already created access token in seconds.
        /// If the elapsed time since the last access token was created is more than the expiration - the safety gap,
        /// then a new token will be created when calling <see cref="OAuthTokenCredential.GetAccessToken()"/>.
        /// </summary>
        public int AccessTokenExpirationSafetyGapInSeconds { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        public OAuthTokenCredential(Dictionary<string, string> config) : this(string.Empty, string.Empty, config)
        {
        }

        /// <summary>
        /// Client Id and Secret for the OAuth
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public OAuthTokenCredential(string clientId, string clientSecret) : this(clientId, clientSecret, null)
        {
        }

        /// <summary>
        /// Client Id and Secret for the OAuth
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="config"></param>
        public OAuthTokenCredential(string clientId = "", string clientSecret = "", Dictionary<string, string> config = null)
        {
            this.config = config != null ? ConfigManager.GetConfigWithDefaults(config) : ConfigManager.GetConfigWithDefaults(ConfigManager.Instance.GetProperties());

            // Set the client ID.
            if(string.IsNullOrEmpty(clientId))
            {
                this.ClientId = this.config.ContainsKey(BaseConstants.ClientId) ? this.config[BaseConstants.ClientId] : string.Empty;
            }
            else
            {
                this.ClientId = clientId;
                this.config[BaseConstants.ClientId] = clientId;
            }

            // Set the client secret.
            if(string.IsNullOrEmpty(clientSecret))
            {
                this.ClientSecret = this.config.ContainsKey(BaseConstants.ClientSecret) ? this.config[BaseConstants.ClientSecret] : string.Empty;
            }
            else
            {
                this.ClientSecret = clientSecret;
                this.config[BaseConstants.ClientSecret] = clientSecret;
            }

            this.SdkVersion = new SDKVersion();
            this.AccessTokenExpirationSafetyGapInSeconds = 120; // Default is 2 minute safety gap for token expiration.
        }

        /// <summary>
        /// Returns the currently cached access token. If no access token was
        /// previously cached, or if the current access token is expired, then
        /// a new one is generated and returned.
        /// </summary>
        /// <returns>The OAuth access token to use for making PayPal requests.</returns>
        /// <exception cref="PayPal.MissingCredentialException">Thrown if clientId or clientSecret are null or empty.</exception>
        /// <exception cref="PayPal.InvalidCredentialException">Thrown if there is an issue converting the credentials to a formatted authorization string.</exception>
        /// <exception cref="PayPal.IdentityException">Thrown if authorization fails as a result of providing invalid credentials.</exception>
        /// <exception cref="PayPal.HttpException">Thrown if authorization fails and an HTTP error response is received.</exception>
        /// <exception cref="PayPal.ConnectionException">Thrown if there is an issue attempting to connect to PayPal's services.</exception>
        /// <exception cref="PayPal.ConfigException">Thrown if there is an error with any informaiton provided by the <see cref="PayPal.Api.ConfigManager"/>.</exception>
        /// <exception cref="PayPal.PayPalException">Thrown for any other general exception. See inner exception for further details.</exception>
        public string GetAccessToken()
        {
            // If the cached access token value is valid, then check to see if
            // it has expired.
            if (!string.IsNullOrEmpty(this.accessToken))
            {
                // If the time since the access token was created is greater
                // than the access token's specified expiration time less the
                // safety gap, then regenerate the token.
                double elapsedSeconds = (DateTime.Now - this.AccessTokenLastCreationDate).TotalSeconds;
                if (elapsedSeconds > this.AccessTokenExpirationInSeconds - this.AccessTokenExpirationSafetyGapInSeconds)
                {
                    this.accessToken = null;
                }
            }

            // If the cached access token is empty or null, then generate a new token.
            if (string.IsNullOrEmpty(this.accessToken))
            {
                this.accessToken = this.GenerateOAuthToken();
            }
            return this.accessToken;
        }

        /// <summary>
        /// Generates a new OAuth token useing the specified client credentials in the authorization request.
        /// </summary>
        /// <returns>The OAuth access token to use for making PayPal requests.</returns>
        private string GenerateOAuthToken()
        {
            var payload = "grant_type=client_credentials";
            var endpoint = this.GetEndpointOverride();

            var apiContext = new APIContext
            {
                Config = this.config,
                SdkVersion = this.SdkVersion,
                HTTPHeaders = new Dictionary<string,string>
                {
                    { BaseConstants.ContentTypeHeader, BaseConstants.ContentTypeHeaderFormUrlEncoded }
                }
            };

            var response = PayPalResource.ConfigureAndExecute<string>(apiContext, PayPalResource.HttpMethod.POST, OAuthTokenPath, payload, endpoint);

            JObject deserializedObject = (JObject)JsonConvert.DeserializeObject(response);
            string generatedToken = (string)deserializedObject["token_type"] + " " + (string)deserializedObject["access_token"];
            this.ApplicationId = (string)deserializedObject["app_id"];
            this.AccessTokenExpirationInSeconds = (int)deserializedObject["expires_in"];
            this.AccessTokenLastCreationDate = DateTime.Now;
            return generatedToken;
        }

        /// <summary>
        /// Gets the overridden endpoint defined in the config, if set.  Otherwise returns an empty string, in which case the default endpoint will be used when requesting a new token.
        /// </summary>
        /// <returns>An endpoint to use; empty string if no override is specified.</returns>
        private string GetEndpointOverride()
        {
            if (this.config.ContainsKey(BaseConstants.OAuthEndpoint))
            {
                var endpoint = this.config[BaseConstants.OAuthEndpoint];
                if (!endpoint.EndsWith("/"))
                {
                    endpoint += "/";
                }
                return endpoint;
            }
            return string.Empty;
        }
    }


}
