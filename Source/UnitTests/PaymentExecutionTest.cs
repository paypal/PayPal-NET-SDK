using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class PaymentExecutionTest
    {
        private Details GetDetails()
        {
            Details amntDetails = new Details();
            amntDetails.tax = "15";
            amntDetails.fee = "2";
            amntDetails.shipping = "10";
            amntDetails.subtotal = "75";
            return amntDetails;
        }

        private Amount GetAmount()
        {
            Amount amnt = new Amount();
            amnt.currency = "USD";
            amnt.details = GetDetails();
            amnt.total = "100";
            return amnt;
        }

        private Transactions GetTransactions()
        {
            Transactions transaction = new Transactions();
            transaction.amount = GetAmount();
            return transaction;
        }

        private ShippingAddress GetShippingAddress()
        {
            ShippingAddress shipping = new ShippingAddress();
            shipping.recipient_name = "PayPalUser";
            return shipping;
        }

        private PayerInfo GetPayerInfo()
        {
            PayerInfo info = new PayerInfo();
            info.first_name = "Joe";
            info.last_name = "Shopper";
            info.email = "Joe.Shopper@email.com";
            info.payer_id = "100";
            info.phone = "12345";
            info.shipping_address = GetShippingAddress();
            return info;
        }

        private PaymentExecution GetPaymentExecution()
        {
            List<Transactions> transactions = new List<Transactions>();
            transactions.Add(GetTransactions());
            PaymentExecution execution = new PaymentExecution();
            execution.payer_id = GetPayerInfo().payer_id;
            execution.transactions = transactions;
            return execution;
        }

        [TestMethod()]
        public void TestPaymentExecution()
        {
            PaymentExecution execution = GetPaymentExecution();
            Assert.AreEqual(execution.payer_id, "100");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            PaymentExecution execution = GetPaymentExecution();
            Assert.IsFalse(execution.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            PaymentExecution execution = GetPaymentExecution();
            Assert.IsFalse(execution.ToString().Length == 0);
        }
    }
}
