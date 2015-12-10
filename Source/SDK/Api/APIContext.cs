using System;
using System.Collections.Generic;

namespace PayPal.Api
{
    /// <summary>
    /// APIContext is used when making HTTP calls to the PayPal REST API.
    /// </summary>
    public class APIContext
    {
        /// <summary>
        /// Initializes a new instance of <seealso cref="APIContext"/> that is used when making HTTP calls to the PayPal REST API.
        /// </summary>
        public APIContext()
        {
            this.ResetRequestId();
            this.SdkVersion = new SDKVersion();
        }

        /// <summary>
        /// Initializes a new instance of <seealso cref="APIContext"/> that is used when making HTTP calls to the PayPal REST API; as well as sets and verifies the state of an <paramref name="accessToken"/>.
        /// </summary>
        /// <param name="accessToken">OAuth access token to use when making API requests</param>
        public APIContext(string accessToken) : this()
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException("accessToken", "accessToken cannot be null");
            }
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Initializes a new instance of <seealso cref="APIContext"/> that is used when making HTTP calls to the PayPal REST API; as well as sets and verifies the states of an <paramref name="accessToken"/> and <paramref name="requestId"/>.
        /// </summary>
        /// <param name="accessToken">OAuth access token to use when making API requests</param>
        /// <param name="requestId">ID used for ensuring idempotency when making a REST API call</param>
        public APIContext(string accessToken, string requestId) : this(accessToken)
        {
            if (string.IsNullOrEmpty(requestId))
            {
                throw new ArgumentNullException("requestId", "requestId cannot be null");
            }
            this.RequestId = requestId;
        }

        /// <summary>
        /// Gets or sets the OAuth access token to use when making API requests.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets whether or not the PayPal-Request-Id header will be set when making API requests, which is used for ensuring idempotency when making API calls.
        /// </summary>
        public bool MaskRequestId { get; set; }

        /// <summary>
        /// Gets or sets the request ID used for ensuring idempotency when making a REST API call.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the PayPal configuration settings to be used when making API requests.
        /// </summary>
        public Dictionary<string, string> Config { get; set; }

        /// <summary>
        /// Gets or sets the HTTP headers to include when making HTTP requests to the API.
        /// </summary>
        public Dictionary<string, string> HTTPHeaders { get; set; }

        /// <summary>
        /// Gets or sets the SDK version to include in the User-Agent header.
        /// </summary>
        public SDKVersion SdkVersion { get; set; }

        /// <summary>
        /// Resets the request ID used for ensuring idempotency when making a REST API call.
        /// </summary>
        public void ResetRequestId()
        {
            this.RequestId = Convert.ToString(Guid.NewGuid());
        }

        /// <summary>
        /// Gets the stored configuration and merges it with the application's default config.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetConfigWithDefaults()
        {
            return ConfigManager.GetConfigWithDefaults(this.Config ?? ConfigManager.Instance.GetProperties());
        }
    }
}
