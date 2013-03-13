using System;

namespace PayPal
{
    /// <summary>
    /// The ApiContext used object to authenticate the call and to send a unique request id (that ensures idempotency).
    /// The SDK generates a request id if you do not pass one explicitly. 
    /// </summary>
    public class APIContext
    {
        /// <summary>
        /// Access Token
        /// </summary>
        private string tokenAccess;
        
        /// <summary>
        /// Request ID
        /// </summary>
        private string requestId;

        /// <summary>
        /// Access Token required for the call
        /// </summary>
        /// <param name="tokenAccess"></param>
        public APIContext(string tokenAccess)
        {
            if (string.IsNullOrEmpty(tokenAccess))
            {
                throw new ArgumentNullException("AccessToken cannot be null");
            }
            this.tokenAccess = tokenAccess;
        }

        /// <summary>
        /// Access Token and Request ID required for the call
        /// </summary>
        /// <param name="tokenAccess"></param>
        /// <param name="requestId"></param>
        public APIContext(string tokenAccess, string requestId): this (tokenAccess)
        {
            if (string.IsNullOrEmpty(requestId))
            {
                throw new ArgumentNullException("RequestId cannot be null");
            }
            this.requestId = requestId;
        }

        public string AccessToken
        {
            get
            {
                return tokenAccess;
            }
        }

        public string RequestID
        {
            get
            {
                if (string.IsNullOrEmpty(requestId))
                {
                    requestId = Convert.ToString(Guid.NewGuid());
                }
                return requestId;
            }
        }
    }
}
