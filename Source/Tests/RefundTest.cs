using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api;
using PayPal;

namespace PayPal.Testing
{
    [TestClass]
    public class RefundTest : BaseTest
    {
        public static Refund GetRefund()
        {
            var refund = new Refund();
            refund.capture_id = "101";
            refund.id = "102";
            refund.parent_payment = "103";
            refund.sale_id = "104";
            refund.state = "COMPLETED";
            refund.amount = AmountTest.GetAmount();
            refund.create_time = TestingUtil.GetCurrentDateISO(-1);
            refund.links = LinksTest.GetLinksList();
            return refund;
        }

        [TestMethod, TestCategory("Unit")]
        public void RefundObjectTest()
        {
            var refund = GetRefund();
            Assert.AreEqual("101", refund.capture_id);
            Assert.AreEqual("102", refund.id);
            Assert.AreEqual("103", refund.parent_payment);
            Assert.AreEqual("104", refund.sale_id);
            Assert.AreEqual("COMPLETED", refund.state);
            Assert.IsNotNull(refund.create_time);
            Assert.IsNotNull(refund.amount);
            Assert.IsNotNull(refund.links);
        }

        [TestMethod, TestCategory("Functional")]
        public void RefundIdTest()
        {
            try
            {
                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var pay = PaymentTest.CreatePaymentAuthorization(apiContext);
                this.RecordConnectionDetails();

                Assert.IsNotNull(pay);
                Assert.IsNotNull(pay.transactions);
                Assert.IsTrue(pay.transactions.Count > 0);
                var transaction = pay.transactions[0];

                Assert.IsNotNull(transaction.related_resources);
                Assert.IsTrue(transaction.related_resources.Count > 0);

                var resource = transaction.related_resources[0];
                Assert.IsNotNull(resource.authorization);

                var authorization = Authorization.Get(apiContext, resource.authorization.id);
                this.RecordConnectionDetails();

                var cap = new Capture
                {
                    amount = new Amount
                    {
                        total = "1",
                        currency = "USD"
                    }
                };
                var response = authorization.Capture(apiContext, cap);
                this.RecordConnectionDetails();

                var fund = new Refund
                {
                    amount = new Amount
                    {
                        total = "1",
                        currency = "USD"
                    }
                };

                apiContext.ResetRequestId();
                var responseRefund = response.Refund(apiContext, fund);
                this.RecordConnectionDetails();

                var retrievedRefund = Refund.Get(apiContext, responseRefund.id);
                this.RecordConnectionDetails();

                Assert.AreEqual(responseRefund.id, retrievedRefund.id);
            }
            catch(ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void RefundNullIdTest()
        {
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => Refund.Get(new APIContext("token"), null));
        }

        [TestMethod, TestCategory("Unit")]
        public void RefundConvertToJsonTest()
        {
            Assert.IsFalse(GetRefund().ConvertToJson().Length == 0);
        }

        [TestMethod, TestCategory("Unit")]
        public void RefundToStringTest()
        {
            Assert.IsFalse(GetRefund().ToString().Length == 0);
        }
    }
}
