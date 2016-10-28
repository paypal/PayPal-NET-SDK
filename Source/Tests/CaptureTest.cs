using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api;
using PayPal;

namespace PayPal.Testing
{
    [TestClass]
    public class CaptureTest : BaseTest
    {
        public static readonly string CaptureJson =
            "{\"amount\":" + AmountTest.AmountJson + "," +
            "\"create_time\":\"" + TestingUtil.GetCurrentDateISO() + "\"," +
            "\"id\":\"001\"," +
            "\"parent_payment\":\"1000\"," +
            "\"state\":\"COMPLETED\"," +
            "\"links\":[" + LinksTest.LinksJson + "]}";

        public static Capture GetCapture()
        {
            return JsonFormatter.ConvertFromJson<Capture>(CaptureJson);
        }

        [TestMethod, TestCategory("Unit")]
        public void CaptureObjectTest()
        {
            var cap = GetCapture();
            var expected = AmountTest.GetAmount();
            var actual = cap.amount;
            Assert.AreEqual(expected.currency, actual.currency);
            Assert.AreEqual(expected.details.fee, actual.details.fee);
            Assert.AreEqual(expected.details.shipping, actual.details.shipping);
            Assert.AreEqual(expected.details.subtotal, actual.details.subtotal);
            Assert.AreEqual(expected.details.tax, actual.details.tax);
            Assert.AreEqual(expected.total, actual.total);
            Assert.IsNotNull(cap.create_time);
            Assert.AreEqual("001", cap.id);
            Assert.AreEqual("1000", cap.parent_payment);
            Assert.AreEqual("COMPLETED", cap.state);
        }

        [TestMethod, TestCategory("Unit")]
        public void CaptureConvertToJsonTest()
        {
            var cap = GetCapture();
            Assert.IsFalse(cap.ConvertToJson().Length == 0);
        }

        [TestMethod, TestCategory("Unit")]
        public void CaptureToStringTest()
        {
            var cap = GetCapture();
            Assert.IsFalse(cap.ToString().Length == 0);
        }

        [TestMethod, TestCategory("Functional")]
        public void CaptureIdTest()
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
                var responseCapture = authorization.Capture(apiContext, cap);
                this.RecordConnectionDetails();

                Assert.IsNotNull(responseCapture);

                var returnCapture = Capture.Get(apiContext, responseCapture.id);
                this.RecordConnectionDetails();

                Assert.AreEqual(responseCapture.id, returnCapture.id);
            }
            catch(ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Functional")]
        public void CaptureRefundTest()
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

                Assert.AreEqual("completed", responseRefund.state);
            }
            catch(ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void CaptureNullIdTest()
        {
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => Capture.Get(new APIContext("token"), null));
        } 
    }
}
