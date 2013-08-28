using System.Collections.Generic;
using PayPal.Api.Payments;
using PayPal;
namespace RestApiSample
{
    public static class Common
    {

       
        // Create an authorized payment
        public static Authorization CreateAuthorization(APIContext apiContext)
        {
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
            details.shipping = "0.03";
            details.subtotal = "107.41";
            details.tax = "0.03";

            // ###Amount
            // Let's you specify a payment amount.
            Amount amnt = new Amount();
            amnt.currency = "USD";
            // Total must be equal to sum of shipping, tax and subtotal.
            amnt.total = "107.47";
            amnt.details = details;

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. Transaction is created with
            // a `Payee` and `Amount` types
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "This is the payment transaction description.";

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // ###FundingInstrument
            // A resource representing a Payeer's funding instrument.
            // Use a Payer ID (A unique identifier of the payer generated
            // and provided by the facilitator. This is required when
            // creating or using a tokenized funding instrument)
            // and the `CreditCardDetails`
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
            // as 'credit_card'
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale`
            Payment pymnt = new Payment();
            pymnt.intent = "authorize";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            // Create a payment by posting to the APIService
            // using a valid APIContext
            Payment createdPayment = pymnt.Create(apiContext);

            return createdPayment.transactions[0].related_resources[0].authorization;
        }

        public static Capture GetCapture(APIContext apiContext, Authorization authorization)
        {
            // ###Amount
            // Let's you specify a capture amount.
            Amount amnt = new Amount();
            amnt.currency = "USD";
            amnt.total = "1.00";

            Capture capture = new Capture();
            capture.amount = amnt;

            // ##IsFinalCapture
            // If set to true, all remaining 
            // funds held by the authorization 
            // will be released in the funding 
            // instrument. Default is ‘false’.
            capture.is_final_capture = true;

            // Capture by POSTing to
            // URI v1/payments/authorization/{authorization_id}/capture
            Capture responseCapture = authorization.Capture(apiContext, capture);

            return responseCapture;
        }

        public static CreditCard SaveCreditCard(APIContext apiContext)
        {
            // ###CreditCard
            // A resource representing a credit card that can be
            // used to fund a payment.
            CreditCard credtCard = new CreditCard();
            credtCard.expire_month = 11;
            credtCard.expire_year = 2018;
            credtCard.number = "4417119669820331";
            credtCard.type = "visa";

            // ###Save
            // Creates the credit card as a resource
            // in the PayPal vault. The response contains
            // an 'id' that you can use to refer to it
            // in the future payments.
            CreditCard createdCreditCard = credtCard.Create(apiContext);
            return createdCreditCard;
        }
    }
}