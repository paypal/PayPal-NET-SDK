using System;
using System.Text;
using BraintreeHttp;

namespace PayPal.Core
{
    /// <summary>
    /// Recommended to use SandboxEnvironment for testing or LiveEnvironment for production.
    /// </summary>
    public class PayPalEnvironment : BraintreeHttp.Environment
    {
        private string baseUrl;
        private string clientId;
        private string clientSecret;
        private string webUrl;

        public PayPalEnvironment(string clientId, string clientSecret, string baseUrl, string webUrl)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.baseUrl = baseUrl;
            this.webUrl = webUrl;
        }

        public string BaseUrl()
        {
            return this.baseUrl;
        }

        public string AuthorizationString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        }

        public string ClientId()
        {
            return clientId;
        }

        public string WebUrl()
        {
            return this.webUrl;
        }
    }

    /// <summary>
    /// Use this environment for testing
    /// </summary>
    public class SandboxEnvironment : PayPalEnvironment
    {
        public SandboxEnvironment(string clientId, string clientSecret) : base(clientId, clientSecret, "https://api.sandbox.paypal.com", "https://www.sandbox.paypal.com")
        { }
    }

    /// <summary>
    /// This is the production environment
    /// </summary>
    public class LiveEnvironment : PayPalEnvironment
    {
        public LiveEnvironment(string clientId, string clientSecret) : base(clientId, clientSecret, "https://api.paypal.com", "https://www.paypal.com")
        { }
    }
}
