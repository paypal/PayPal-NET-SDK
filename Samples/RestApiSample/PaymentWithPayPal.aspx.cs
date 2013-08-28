// #Create Payment Using PayPal Sample
// This sample code demonstrates how you can process a 
// PayPal Account based Payment.
// API used: /v1/payments/payment
using System;
using System.Web;
using PayPal;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestApiSample
{
    public partial class PaymentWithPayPal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
            Payment pymnt = null;

            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
             // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext..
            APIContext apiContext = Configuration.GetAPIContext();

            // ## ExecutePayment
            if (Request.Params["PayerID"] != null)
            {
                pymnt = new Payment();
                if (Request.Params["guid"] != null)
                {
                    pymnt.id = (string)Session[Request.Params["guid"]];

                }
                try
                {
                    PaymentExecution pymntExecution = new PaymentExecution();
                    pymntExecution.payer_id = Request.Params["PayerID"];

                    Payment executedPayment = pymnt.Execute(apiContext, pymntExecution);
                    CurrContext.Items.Add("ResponseJson", JObject.Parse(executedPayment.ConvertToJson()).ToString(Formatting.Indented));
                }
                catch (PayPal.Exception.PayPalException ex)
                {
                    CurrContext.Items.Add("Error", ex.Message);
                }
            }

            // ## Creating Payment
            else
            {
                // ###Items
                // Items within a transaction.
                Item item = new Item();
                item.name = "Item Name";
                item.currency = "USD";
                item.price = "15";
                item.quantity = "5";
                item.sku = "sku";

                List<Item> itms = new List<Item>();
                itms.Add(item);
                ItemList itemList = new ItemList();
                itemList.items = itms;

                // ###Payer
                // A resource representing a Payer that funds a payment
                // Payment Method
                // as `paypal`
                Payer payr = new Payer();
                payr.payment_method = "paypal";
                Random rndm = new Random();
                var guid = Convert.ToString(rndm.Next(100000));

                string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PaymentWithPayPal.aspx?";

                // # Redirect URLS
                RedirectUrls redirUrls = new RedirectUrls();
                redirUrls.cancel_url = baseURI + "guid=" + guid;
                redirUrls.return_url = baseURI + "guid=" + guid;

                // ###Details
                // Let's you specify details of a payment amount.
                Details details = new Details();
                details.tax = "15";
                details.shipping = "10";
                details.subtotal = "75";

                // ###Amount
                // Let's you specify a payment amount.
                Amount amnt = new Amount();
                amnt.currency = "USD";
                // Total must be equal to sum of shipping, tax and subtotal.
                amnt.total = "100";
                amnt.details = details;

                // ###Transaction
                // A transaction defines the contract of a
                // payment - what is the payment for and who
                // is fulfilling it. 
                List<Transaction> transactionList = new List<Transaction>();
                Transaction tran = new Transaction();
                tran.description = "Transaction description.";
                tran.amount = amnt;
                tran.item_list = itemList;
                // The Payment creation API requires a list of
                // Transaction; add the created `Transaction`
                // to a List
                transactionList.Add(tran);

                // ###Payment
                // A Payment Resource; create one using
                // the above types and intent as `sale` or `authorize`
                pymnt = new Payment();
                pymnt.intent = "sale";
                pymnt.payer = payr;
                pymnt.transactions = transactionList;
                pymnt.redirect_urls = redirUrls;

                try
                {
                    // Create a payment using a valid APIContext
                    Payment createdPayment = pymnt.Create(apiContext);

                    CurrContext.Items.Add("ResponseJson", JObject.Parse(createdPayment.ConvertToJson()).ToString(Formatting.Indented));

                    var links = createdPayment.links.GetEnumerator();

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            CurrContext.Items.Add("RedirectURL", lnk.href);
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                }
                catch (PayPal.Exception.PayPalException ex)
                {
                    CurrContext.Items.Add("Error", ex.Message);
                }
            }
            CurrContext.Items.Add("RequestJson", JObject.Parse(pymnt.ConvertToJson()).ToString(Formatting.Indented));

            Server.Transfer("~/Response.aspx");

        }
    }
}
