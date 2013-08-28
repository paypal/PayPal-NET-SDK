// #AuthorizationCapture Sample
// The sample code demonstrates
// how to capture a previously
// authorized payment. 
// API used: POST /v1/payments/authorization/{authorization_id}/capture
using System;
using System.Web;
using PayPal.Api.Payments;
using PayPal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestApiSample
{
    public partial class AuthorizationCapture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
            Capture capture = null;
            try
            {
                // ### Api Context
                // Pass in a `APIContext` object to authenticate 
                // the call and to send a unique request id 
                // (that ensures idempotency). The SDK generates
                // a request id if you do not pass one explicitly. 
                // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
                APIContext apiContext = Configuration.GetAPIContext();
                                
                // ###Authorization
                // Retrieve a Authorization object
                // by making a Payment with intent
                // as `authorize`
                Authorization authorization = Common.CreateAuthorization(apiContext);

                // ###Amount
                // Let's you specify a capture amount.
                Amount amnt = new Amount();
                amnt.currency = "USD";
                amnt.total = "4.54";

                capture = new Capture();
                capture.amount = amnt;

                // ##IsFinalCapture
                // If set to true, all remaining 
                // funds held by the authorization 
                // will be released in the funding 
                // instrument. Default is `false`.
                capture.is_final_capture = true;                      
                
                // Capture an authorized payment by POSTing to
                // URI v1/payments/authorization/{authorization_id}/capture
                Capture responseCapture = authorization.Capture(apiContext, capture);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(responseCapture.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }
            CurrContext.Items.Add("RequestJson", JObject.Parse(capture.ConvertToJson()).ToString(Formatting.Indented));

            Server.Transfer("~/Response.aspx");

        }

    }
}
