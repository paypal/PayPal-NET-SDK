// #GetCapture Sample
// This sample code demonstrates how to 
// retrieve a Capture resource
// API used: GET /v1/payments/capture/{capture_id}
using PayPal.Api;
using System.Collections.Generic;

namespace PayPal.Sample
{
    public partial class GetCapture : BaseSamplePage
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

            #region Track Workflow
            //--------------------
            this.flow.AddNewRequest("Get capture details", description: "ID: " + captureId);
            //--------------------
            #endregion

            // Retrieve the Capture object by
            // doing a GET call to 
            // URI v1/payments/capture/{capture_id}
            var capture = Capture.Get(apiContext, captureId);

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.RecordResponse(capture);
            #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }
    }
}
