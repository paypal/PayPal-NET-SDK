// #CreateCreditCard Sample
// Using the 'vault' API, you can store a 
// Credit Card securely on PayPal. You can
// use a saved Credit Card to process
// a payment in the future.
// The following code demonstrates how 
// can save a Credit Card on PayPal using 
// the Vault API.
// API used: POST /v1/vault/credit-card
using System;
using System.Web;
using PayPal;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RestApiSample
{
    public partial class CreateCreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;

            // ###CreditCard
            // A resource representing a credit card that can be
            // used to fund a payment.
            CreditCard credtCard = new CreditCard();
            credtCard.expire_month = 11;
            credtCard.expire_year = 2018;
            credtCard.number = "4417119669820331";
            credtCard.type = "visa";

            try
            {
                 // ### Api Context
                 // Pass in a `APIContext` object to authenticate 
                 // the call and to send a unique request id 
                 // (that ensures idempotency). The SDK generates
                 // a request id if you do not pass one explicitly. 
                  // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext..
                APIContext apiContext = Configuration.GetAPIContext();

                // ###Save
                // Creates the credit card as a resource
                // in the PayPal vault. The response contains
                // an 'id' that you can use to refer to it
                // in the future payments.
                CreditCard createdCreditCard = credtCard.Create(apiContext);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(createdCreditCard.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }

            CurrContext.Items.Add("RequestJson", JObject.Parse(credtCard.ConvertToJson()).ToString(Formatting.Indented));

            Server.Transfer("~/Response.aspx");
        }
    }
}
