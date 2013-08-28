using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class TransactionsTest
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

        private Transactions CreateTransactions()
        {
            Transactions transaction = new Transactions();
            transaction.amount = GetAmount();
            return transaction;
        }

        [TestMethod()]
        public void TestTransactions()
        {
            Transactions transaction = CreateTransactions();
            Assert.AreEqual(transaction.amount.currency, "USD");
            Assert.AreEqual(transaction.amount.details.tax, "15");
            Assert.AreEqual(transaction.amount.details.fee, "2");
            Assert.AreEqual(transaction.amount.details.shipping, "10");
            Assert.AreEqual(transaction.amount.details.subtotal, "75");
            Assert.AreEqual(transaction.amount.total, "100");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Transactions trans = CreateTransactions();
            Assert.IsFalse(trans.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Transactions trans = CreateTransactions();
            Assert.IsFalse(trans.ToString().Length == 0);
        }
    }
}
