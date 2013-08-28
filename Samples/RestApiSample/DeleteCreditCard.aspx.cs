// #DeleteCreditCard Sample
// This sample code demonstrates how you 
// delete a previously saved 
// Credit Card using the 'vault' API.
// API used: DELETE /v1/vault/credit-card/{id}
using System;
using System.Web;
using PayPal;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RestApiSample
{
    public partial class DeleteCreditCard : System.Web.UI.Page
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

                CreditCard card = CreditCard.Get(apiContext, Common.SaveCreditCard(apiContext).id);

                 // Delete the credit card
                card.Delete(apiContext);
                CurrContext.Items.Add("ResponseJson", "CreditCard deleted successfully");
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }

            Server.Transfer("~/Response.aspx");
        }
    }
}
