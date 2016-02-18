using System.Collections.Generic;
using System.Web;

namespace PayPal.Api
{
    /// <summary>
    /// Class for storing user information for OpenIdConnect API calls.
    /// </summary>
    public class UserinfoParameters
    {
        /// <summary>
        /// Schema used in query parameters
        /// </summary>
        private const string Schema = "schema";

        /// <summary>
        /// Access Token used in query parameters
        /// </summary>
        private const string AccessToken = "access_token";

        /// <summary>
        /// Constructor
        /// </summary>
        public UserinfoParameters()
        {
            this.ContainerMap = new Dictionary<string, string>();
            this.ContainerMap.Add(UserinfoParameters.Schema, "openid");
        }

        /// <summary>
        /// Gets and sets the backing map
        /// </summary>
        public Dictionary<string, string> ContainerMap { get; set; }

        /// <summary>
        /// Set the Access Token
        /// </summary>
        /// <param name="accessToken"></param>
        public void SetAccessToken(string accessToken)
        {
            this.ContainerMap[UserinfoParameters.AccessToken] = HttpUtility.UrlEncode(accessToken);
        }
    }
}
