// #SaleRefund Sample
// This sample code demonstrate how you can 
// process a refund on a sale transaction created 
// using the Payments API.
// API used: /v1/payments/sale/{sale-id}/refund
using System;
using System.Web;
using PayPal;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RestApiSample
{
    public partial class SaleRefund : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;

            // ###Amount
            // Create an Amount object to
            // represent the amount to be
            // refunded. Create the refund object, if the refund is partial
            Amount amount = new Amount();
            amount.currency = "USD";
            amount.total = "0.01";

            // ###Refund
            // A refund transaction.
            // Use the amount to create
            // a refund object
            Refund refund = new Refund();
            refund.amount = amount;
            // ###Sale
            // A sale transaction.
            // Create a Sale object with the
            // given sale transaction id.
            Sale sale = new Sale();
            sale.id = "7X350557WR366683S";
            try
            {
                // ### Api Context
                // Pass in a `APIContext` object to authenticate 
                // the call and to send a unique request id 
                // (that ensures idempotency). The SDK generates
                // a request id if you do not pass one explicitly. 
                 // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext..
                APIContext apiContext = Configuration.GetAPIContext();

                // Refund by posting Refund object using a valid APIContext
                Refund refundedSale = sale.Refund(apiContext, refund);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(refundedSale.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }
            CurrContext.Items.Add("RequestJson",
                  JObject.Parse(refund.ConvertToJson()).ToString(Formatting.Indented));
            Server.Transfer("~/Response.aspx");
        }
    }
}
