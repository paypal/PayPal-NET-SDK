// #AuthorizationCapture Sample
// The sample code demonstrates
// how to capture a previously
// authorized payment. 
// API used: POST /v1/payments/authorization/{authorization_id}/capture
using PayPal.Api;
using PayPal.Sample.Utilities;
using System.Collections.Generic;

namespace PayPal.Sample
{
    public partial class AuthorizationCapture : BaseSamplePage
    {
        protected override void RunSample()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();
            var authorizationId = "<your authorization id here>";
  
            // ###Authorization
            // Once the payment with intent set to `authorize` has been created, retrieve its authorization object.
            var authorization = Authorization.Get(apiContext, authorizationId);

            // Specify an amount to capture.  By setting 'is_final_capture' to true, all remaining funds held by the authorization will be released from the funding instrument.
            var capture = new Capture()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    total = "100.00"
                },
                is_final_capture = true
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.AddNewRequest("Capture authorized payment", capture, string.Format("URI: v1/payments/authorization/{0}/capture", authorization.id));
            #endregion

            // Capture an authorized payment by POSTing to
            // URI v1/payments/authorization/{authorization_id}/capture
            var responseCapture = authorization.Capture(apiContext, capture);

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.RecordResponse(responseCapture);
            #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }
    }
}
