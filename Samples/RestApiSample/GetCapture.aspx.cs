// #GetCapture Sample
// This sample code demonstrates how to 
// retrieve a Capture resource
// API used: GET /v1/payments/capture/{capture_id}
using System;
using System.Web;
using PayPal.Api.Payments;
using PayPal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestApiSample
{
    public partial class GetCapture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
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
                // as `authorize`
                Authorization authorization = Common.CreateAuthorization(apiContext);

                /// ###Capture
                // Create a Capture object
                // by doing a capture on
                // Authorization object
                // and retrieve the Id
                Capture capture = Common.GetCapture(apiContext, authorization);

                // Retrieve the Capture object by
                // doing a GET call to 
                // URI v1/payments/capture/{capture_id}
                capture = Capture.Get(apiContext, capture.id);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(capture.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }

            Server.Transfer("~/Response.aspx");
        }
    }
}
