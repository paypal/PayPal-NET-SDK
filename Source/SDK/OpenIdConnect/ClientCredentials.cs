
namespace PayPal.Api
{
    /// <summary>
    /// REST application client credentials.
    /// </summary>
    public abstract class ClientCredentials
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public string clientId;

        /// <summary>
        /// Client Secret
        /// </summary>
        public string clientSecret;

        /// <summary>
        /// Set the Client ID
        /// </summary>
        /// <param name="clientId"></param>
        public void setClientId(string clientId)
        {
            this.clientId = clientId;
        }

        /// <summary>
        /// Set the Client Secret
        /// </summary>
        /// <param name="clientSecret"></param>
        public void setClientSecret(string clientSecret)
        {
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Returns the Client ID
        /// </summary>
        /// <returns></returns>
        public string getClientId()
        {
            return this.clientId;
        }

        /// <summary>
        /// Returns the Client Secret
        /// </summary>
        /// <returns></returns>
        public string getClientSecret()
        {
            return this.clientSecret;
        }
    }
}
