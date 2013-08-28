// #RefundCapture Sample
// This sample code demonstrates how to do a 
// Refund on a Capture resource
// API used: POST /v1/payments/capture/{capture_id}/refund
using System;
using System.Web;
using PayPal.Api.Payments;
using PayPal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestApiSample
{
    public partial class RefundCapture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
            Refund refund = null;
            try
            {

                // ### Api Context
                // Pass in a `APIContext` object to authenticate 
                // the call and to send a unique request id 
                // (that ensures idempotency). The SDK generates
                // a request id if you do not pass one explicitly. 
                 // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext..
                APIContext apiContext = Configuration.GetAPIContext();

                // ###Authorization
                // Retrieve a Authorization object
                // by making a Payment with intent
                // as 'authorize'
                Authorization authorization = Common.CreateAuthorization(apiContext);

                /// ###Capture
                // Create a Capture object
                // by doing a capture on
                // Authorization object
                Capture capture = Common.GetCapture(apiContext, authorization);

                /// ###Refund
                /// Create a Refund object
                refund = new Refund();

                // ###Amount
                // Let's you specify a capture amount.
                Amount refundAmount = new Amount();
                refundAmount.currency = "USD";
                refundAmount.total = "0.50";

                refund.amount = refundAmount;

                // Do a Refund by
                // POSTing to 
                // URI v1/payments/capture/{capture_id}/refund
                Refund responseRefund = capture.Refund(Configuration.GetAPIContext(), refund);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(responseRefund.ConvertToJson()).ToString(Formatting.Indented));

            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }
            CurrContext.Items.Add("RequestJson", JObject.Parse(refund.ConvertToJson()).ToString(Formatting.Indented));
            Server.Transfer("~/Response.aspx");
        }
    }
}
