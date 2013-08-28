// #CreatePayment using credit card Sample
// This sample code demonstrate how you can process
// a payment with a credit card.
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
    public partial class PaymentWithCreditCard : System.Web.UI.Page
    {
        // ##Create
        // Sample showing to create a Payment using
        // CreditCard as a FundingInstrument
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;

            // ###Items
            // Items within a transaction.
            Item item = new Item();
            item.name = "Item Name";
            item.currency = "USD";
            item.price = "1";
            item.quantity = "5";
            item.sku = "sku";

            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            // ###Address
            // Base Address object used as shipping or billing
            // address in a payment.
            Address billingAddress = new Address();
            billingAddress.city = "Johnstown";
            billingAddress.country_code = "US";
            billingAddress.line1 = "52 N Main ST";
            billingAddress.postal_code = "43210";
            billingAddress.state = "OH";

            // ###CreditCard
            // A resource representing a credit card that can be
            // used to fund a payment.
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = "874";
            crdtCard.expire_month = 11;
            crdtCard.expire_year = 2018;
            crdtCard.first_name = "Joe";
            crdtCard.last_name = "Shopper";
            crdtCard.number = "4417119669820331";
            crdtCard.type = "visa";

            // ###Details
            // Let's you specify details of a payment amount.
            Details details = new Details();
            details.shipping = "1";
            details.subtotal = "5";
            details.tax = "1";

            // ###Amount
            // Let's you specify a payment amount.
            Amount amnt = new Amount();
            amnt.currency = "USD";
            // Total must be equal to sum of shipping, tax and subtotal.
            amnt.total = "7";
            amnt.details = details;

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. 
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "This is the payment transaction description.";
            tran.item_list = itemList;

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // ###FundingInstrument
            // A resource representing a Payer's funding instrument.
            // For direct credit card payments, set the CreditCard
            // field on this object.
            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of
            // FundingInstrument; add the created `FundingInstrument`
            // to a List
            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // ###Payer
            // A resource representing a Payer that funds a payment
            // Use the List of `FundingInstrument` and the Payment Method
            // as `credit_card`
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale` or `authorize`
            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                // ### Api Context
                // Pass in a `APIContext` object to authenticate 
                // the call and to send a unique request id 
                // (that ensures idempotency). The SDK generates
                // a request id if you do not pass one explicitly. 
                 // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext..
                APIContext apiContext = Configuration.GetAPIContext();

                // Create a payment using a valid APIContext
                Payment createdPayment = pymnt.Create(apiContext);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(createdPayment.ConvertToJson()).ToString(Formatting.Indented));
            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }
            CurrContext.Items.Add("RequestJson", JObject.Parse(pymnt.ConvertToJson()).ToString(Formatting.Indented));

            Server.Transfer("~/Response.aspx");
        }
    }
}
