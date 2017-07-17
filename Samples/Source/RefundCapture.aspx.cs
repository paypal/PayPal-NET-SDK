// #RefundCapture Sample
// This sample code demonstrates how to do a 
// Refund on a Capture resource
// API used: POST /v1/payments/capture/{capture_id}/refund
using PayPal.Api;
using System.Collections.Generic;

namespace PayPal.Sample
{
    public partial class RefundCapture : BaseSamplePage
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
            var captureId = "<your capture id here>";

 
            // Capture by POSTing to
            // URI v1/payments/authorization/{authorization_id}/capture
            var capture = Capture.Get(apiContext, captureId);

            // ###Refund
            // Create a Refund object
            var refund = new Refund()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    total = "100.00"
                }
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.AddNewRequest("Capture refund", refund, "ID: " + captureId);
            #endregion

            // Do a Refund by POSTing to 
            // URI v1/payments/capture/{capture_id}/refund
            var response = capture.Refund(Configuration.GetAPIContext(), refund);

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.RecordResponse(response);
            #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }
    }
}
