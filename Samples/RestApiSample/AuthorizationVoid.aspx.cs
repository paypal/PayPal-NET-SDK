// #AuthorizationVoid Sample
// The sample code demonstrates
// how to void an
// authorized payment.
// API used: POST /v1/payments/authorization/{authorization_id}/void 
using System;
using System.Web;
using PayPal.Api.Payments;
using PayPal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestApiSample
{
    public partial class AuthorizationVoid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
            Authorization authorization = null;
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
                // Create an Authorization 
                // by making a Payment with intent
                // as 'authorize'
                authorization = Common.CreateAuthorization(apiContext);

                // Void an Authorization
                // by POSTing to 
                // URI v1/payments/authorization/{authorization_id}/void
                Authorization returnAuthorization = authorization.Void(apiContext);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(returnAuthorization.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }
            
            Server.Transfer("~/Response.aspx");

        }

        
    }
}
