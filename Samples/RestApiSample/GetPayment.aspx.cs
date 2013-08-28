// #GetPayment Sample
// This sample code demonstrates how you can retrieve
// the details of a payment resource.
// API used: /v1/payments/payment/{payment-i
using System;
using System.Web;
using PayPal;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RestApiSample
{
    public partial class GetPayment : System.Web.UI.Page
    {
        // ##GetPaymentByPaymentId
        // Call the method with a valid Payment ID
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
                
                // Retrieve the payment object by calling the
                // static `Get` method
                // on the Payment class by passing a valid
                // APIContext and Payment ID
                Payment pymnt = Payment.Get(apiContext, "PAY-0XL713371A312273YKE2GCNI");

                CurrContext.Items.Add("ResponseJson", JObject.Parse(pymnt.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }
            Server.Transfer("~/Response.aspx");
        }
    }
}
