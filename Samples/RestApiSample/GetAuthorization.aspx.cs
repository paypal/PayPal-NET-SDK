// #GetAuthorization Sample
// This sample code demonstrates how to 
// retrieve an Authorization resource
// API used: GET /v1/payments/authorization/{id}
using System;
using System.Web;
using PayPal.Api.Payments;
using PayPal;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestApiSample
{
    public partial class GetAuthorization : System.Web.UI.Page
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
                // Retrieve an Authorization Id
                // by making a Payment with intent
                // as 'authorize' and parsing through
                // the Payment object
                string authorizationId = Common.CreateAuthorization(apiContext).id;

                // Get Authorization by sending
                // a GET request with authorization Id
                // to the
                // URI v1/payments/authorization/{id}
                Authorization authorization = Authorization.Get(apiContext, authorizationId);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(authorization.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }

            Server.Transfer("~/Response.aspx");
        }

    }
}
