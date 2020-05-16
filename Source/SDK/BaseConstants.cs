using System.Text;
using PayPal.Util;

namespace PayPal.Api
{
    /// <summary>
    /// Constants that are used by the PayPal SDK.
    /// </summary>
    public static class BaseConstants
    {
        /// <summary>
        /// PayPal webhook transmission ID HTTP request header
        /// </summary>
        public const string PayPalTransmissionIdHeader = "PAYPAL-TRANSMISSION-ID";

        /// <summary>
        /// PayPal webhook transmission time HTTP request header
        /// </summary>
        public const string PayPalTransmissionTimeHeader = "PAYPAL-TRANSMISSION-TIME";

        /// <summary>
        /// PayPal webhook transmission signature HTTP request header
        /// </summary>
        public const string PayPalTransmissionSignatureHeader = "PAYPAL-TRANSMISSION-SIG";

        /// <summary>
        /// PayPal webhook certificate URL HTTP request header
        /// </summary>
        public const string PayPalCertificateUrlHeader = "PAYPAL-CERT-URL";

        /// <summary>
        /// PayPal webhook authentication algorithm HTTP request header
        /// </summary>
        public const string PayPalAuthAlgorithmHeader = "PAYPAL-AUTH-ALGO";

        /// <summary>
        /// Allowed application mode - live
        /// </summary>
        public const string LiveMode = "live";

        /// <summary>
        /// Allowed application mode - production   //same as live. added to match value used in other places (e.g. checkout.js)
        /// </summary>
        public const string ProductionMode = "production";

        /// <summary>
        /// Allowed application mode - sandbox
        /// </summary>
        public const string SandboxMode = "sandbox";

        /// <summary>
        /// Allowed application mode - security-test-sandbox
        /// </summary>
        public const string SecurityTestSandboxMode = "security-test-sandbox";

        /// <summary>
        /// Sandbox REST API endpoint
        /// </summary>
        public const string RESTSandboxEndpoint = "https://api.sandbox.paypal.com/";

        /// <summary>
        /// Live REST API endpoint
        /// </summary>
        public const string RESTLiveEndpoint = "https://api.paypal.com/";

        /// <summary>
        /// Security Test Sandbox REST API endpoint
        /// </summary>
        public const string RESTSecurityTestSandoxEndpoint = "https://test-api.sandbox.paypal.com/";

        /// <summary>
        /// Configuration key for application mode
        /// </summary>
        public const string ApplicationModeConfig = "mode";

        /// <summary>
        /// Configuration key for End point
        /// </summary>
        public const string EndpointConfig = "endpoint";

        /// <summary>
        /// Configuration key for HTTP Proxy Address
        /// </summary>
        public const string HttpProxyAddressConfig = "proxyAddress";

        /// <summary>
        /// Configuration key for HTTP Proxy Credential
        /// </summary>
        public const string HttpProxyCredentialConfig = "proxyCredentials";

        /// <summary>
        /// Configuration key for HTTP Connection Timeout
        /// </summary>
        public const string HttpConnectionTimeoutConfig = "connectionTimeout";

        /// <summary>
        /// Configuration key for HTTP Connection Retry
        /// </summary>
        public const string HttpConnectionRetryConfig = "requestRetries";

        /// <summary>
        /// Configuration key suffix for Client Id
        /// </summary>
        public const string ClientId = "clientId";

        /// <summary>
        /// Configuration key suffix for Client Secret
        /// </summary>
        public const string ClientSecret = "clientSecret";

        /// <summary>
        /// OAuth endpoint config key
        /// </summary>
        public const string OAuthEndpoint = "oauth.EndPoint";

        /// <summary>
        /// Trusted certificate file location to be used when validating webhook event certifcates.
        /// </summary>
        public const string TrustedCertificateLocation = "webhook.trustCert";

        /// <summary>
        /// Webhook ID used when validating received webhook notifications.
        /// </summary>
        public const string WebhookIdConfig = "webhook.id";

        /// <summary>
        /// User Agent HTTP Header
        /// </summary>
        public const string UserAgentHeader = "User-Agent";

        /// <summary>
        /// Content Type HTTP Header
        /// </summary>
        public const string ContentTypeHeader = "Content-Type";

        /// <summary>
        /// Application - Json Content Type
        /// </summary>
        public const string ContentTypeHeaderJson = "application/json";

        /// <summary>
        /// Application - Form URL Encoded Content Type
        /// </summary>
        public const string ContentTypeHeaderFormUrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// Authorization HTTP Header
        /// </summary>
        public const string AuthorizationHeader = "Authorization";

        /// <summary>
        /// PayPal Request Id HTTP Header
        /// </summary>
        public const string PayPalRequestIdHeader = "PayPal-Request-Id";

        /// <summary>
        /// The version of this SDK.
        /// </summary>
        public static string SdkVersion { get { return SDKUtil.GetAssemblyVersionForType(typeof(BaseConstants)); } }

        /// <summary>
        /// The name of this SDK.
        /// </summary>
        public const string SdkName = "PayPal-NET-SDK";

        /// <summary>
        /// Defines string constants for HATEOAS link relations.
        /// </summary>
        public static class HateoasLinkRelations
        {
            /// <summary>
            /// HATEOAS link relation used to get the details of the current resource.
            /// </summary>
            public const string Self = "self";

            /// <summary>
            /// HATEOAS link relation used to get the details of the parent payment of a payment resource.
            /// </summary>
            public const string ParentPayment = "parent_payment";

            /// <summary>
            /// HATEOAS link relation used to update the current resource.
            /// </summary>
            public const string Update = "update";

            /// <summary>
            /// HATEOAS link relation used to delete the current resource.
            /// </summary>
            public const string Delete = "delete";

            /// <summary>
            /// HATEOAS link relation used to patch the current resource.
            /// </summary>
            public const string Patch = "patch";

            /// <summary>
            /// HATEOAS link relation used to redirect a buyer to PayPal to provide approval for the payment associated with the current resource.
            /// </summary>
            public const string ApprovalUrl = "approval_url";

            /// <summary>
            /// HATEOAS link relation used to execute the payment associated with the current resource.
            /// </summary>
            public const string Execute = "execute";

            /// <summary>
            /// HATEOAS link relation used to capture the payment associated with the current resource.
            /// </summary>
            public const string Capture = "capture";

            /// <summary>
            /// HATEOAS link relation used to provide authorization for the payment associated with the current resource.
            /// </summary>
            public const string Authorization = "authorization";

            /// <summary>
            /// HATEOAS link relation used to get the order resource associated with the current resource.
            /// </summary>
            public const string Order = "order";

            /// <summary>
            /// HATEOAS link relation used to issue a refund for the current resource.
            /// </summary>
            public const string Refund = "refund";

            /// <summary>
            /// HATEOAS link relation used to void the current resource.
            /// </summary>
            public const string Void = "void";

            /// <summary>
            /// HATEOAS link relation used to reauthorize a payment authorization resource.
            /// </summary>
            public const string ReAuthorize = "reauthorize";

            /// <summary>
            /// HATEOAS link relation used for searches and provides results for the next set of search results.
            /// </summary>
            public const string NextPage = "next_page";

            /// <summary>
            /// HATEOAS link relation used for searches and provides results for the previous set of search results.
            /// </summary>
            public const string PreviousPage = "previous_page";

            /// <summary>
            /// HATEOAS link relation used for searches and provides results for the first set of search results.
            /// </summary>
            public const string Start = "start";

            /// <summary>
            /// HATEOAS link relation used for searches and provides results for the last set of search results.
            /// </summary>
            public const string Last = "last";

            /// <summary>
            /// HATEOAS link relation used to suspend a billing agreement.
            /// </summary>
            public const string Suspend = "suspend";

            /// <summary>
            /// HATEOAS link relation used to reactivate a billing agreement.
            /// </summary>
            public const string ReActivate = "re_activate";

            /// <summary>
            /// HATEOAS link relation used to cancel a billing agreement.
            /// </summary>
            public const string Cancel = "cancel";

            /// <summary>
            /// HATEOAS link relation used to get the payout batch associated with the current payout item resource.
            /// </summary>
            public const string Batch = "batch";

            /// <summary>
            /// HATEOAS link relation used to get a specific payout item resource associated with current payout batch.
            /// </summary>
            public const string Item = "item";

            /// <summary>
            /// HATEOAS link relation used to resend a webhook event.
            /// </summary>
            public const string Resend = "resend";

            /// <summary>
            /// HATEOAS link relation used for webhook searches and provides results for the next set of search results.
            /// </summary>
            public const string Next = "next";

            /// <summary>
            /// HATEOAS link relation used for webhook searches and provides results for the previous set of search results.
            /// </summary>
            public const string Previous = "previous";
        }
    }
}
