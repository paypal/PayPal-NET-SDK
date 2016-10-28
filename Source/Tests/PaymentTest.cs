using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api;
using PayPal;
using PayPal.Util;
using System;
using System.Linq;

namespace PayPal.Testing
{
    [TestClass]
    public class PaymentTest : BaseTest
    {
        public static Payment GetPaymentAuthorization()
        {
            return GetPaymentUsingCreditCard("authorize");
        }

        public static Payment GetPaymentForSale()
        {
            return GetPaymentUsingCreditCard("sale");
        }

        public static Payment GetPaymentOrder()
        {
            return GetPaymentUsingPayPal("order");
        }

        private static Payment GetPaymentUsingCreditCard(string intent)
        {
            var payment = new Payment();
            payment.intent = intent;
            payment.transactions = TransactionTest.GetTransactionList();
            payment.transactions[0].amount.details = null;
            payment.transactions[0].payee = null;
            payment.payer = PayerTest.GetPayerUsingCreditCard();
            payment.payer.payer_info.phone = null;
            payment.redirect_urls = RedirectUrlsTest.GetRedirectUrls();
            return payment;
        }

        private static Payment GetPaymentUsingPayPal(string intent)
        {
            var payment = new Payment();
            payment.intent = intent;
            payment.transactions = TransactionTest.GetTransactionList();
            payment.transactions[0].amount.details = null;
            payment.transactions[0].payee = null;
            payment.transactions[0].item_list.shipping_address = null;
            payment.payer = PayerTest.GetPayerUsingPayPal();
            payment.redirect_urls = RedirectUrlsTest.GetRedirectUrls();
            return payment;
        }

        public static Payment CreateFuturePayment()
        {
            FuturePayment pay = new FuturePayment();
            pay.intent = "sale";
            CreditCard card = CreditCardTest.GetCreditCard();
            List<FundingInstrument> fundingInstruments = new List<FundingInstrument>();
            FundingInstrument fundingInstrument = new FundingInstrument();
            fundingInstrument.credit_card = card;
            fundingInstruments.Add(fundingInstrument);
            Payer payer = new Payer();
            payer.payment_method = "credit_card";
            payer.funding_instruments = fundingInstruments;
            List<Transaction> transactionList = new List<Transaction>();
            Transaction trans = new Transaction();
            trans.amount = AmountTest.GetAmount();
            transactionList.Add(trans);
            pay.transactions = transactionList;
            pay.payer = payer;
            Payment paymnt = pay.Create(TestingUtil.GetApiContext());
            return paymnt;
        }

        public static Payment CreatePaymentAuthorization(APIContext apiContext)
        {
            return GetPaymentAuthorization().Create(apiContext);
        }

        public static Payment CreatePaymentForSale(APIContext apiContext)
        {
            return GetPaymentForSale().Create(apiContext);
        }

        public static Payment CreatePaymentOrder(APIContext apiContext)
        {
            return GetPaymentOrder().Create(apiContext);
        }

        #region Unit Tests
        [TestMethod, TestCategory("Unit")]
        public void PaymentNullAccessToken()
        {
            var payment = GetPaymentForSale();
            string accessToken = null;
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => payment.Create(new APIContext(accessToken)));
        }
        #endregion

        #region Functional Tests
        [TestMethod, TestCategory("Functional")]
        public void PaymentStateTest()
        {
            try
            {
                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var actual = CreatePaymentForSale(apiContext);
                this.RecordConnectionDetails();

                Assert.AreEqual("approved", actual.state);
            }
            catch(ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Functional")]
        public void PaymentCreateAndGetTest()
        {
            try
            {
                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var pay = CreatePaymentForSale(apiContext);
                this.RecordConnectionDetails();

                var retrievedPayment = Payment.Get(apiContext, pay.id);
                this.RecordConnectionDetails();

                Assert.AreEqual(pay.id, retrievedPayment.id);
            }
            catch (ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Functional")]
        public void PaymentListHistoryTest()
        {
            try
            {
                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var paymentHistory = Payment.List(apiContext, count: 10);
                this.RecordConnectionDetails();

                Assert.IsTrue(paymentHistory.count >= 0 && paymentHistory.count <= 10);
            }
            catch (ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Functional")]
        public void FuturePaymentTest()
        {
            try
            {
                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var futurePayment = CreateFuturePayment();
                this.RecordConnectionDetails();

                var retrievedPayment = FuturePayment.Get(apiContext, futurePayment.id);
                this.RecordConnectionDetails();

                Assert.AreEqual(futurePayment.id, retrievedPayment.id);
            }
            catch (ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Functional")]
        public void PaymentVerifyCreatePayPalPaymentForSaleResponse()
        {
            try
            {
                var deserializationErrors = new List<string>();
                JsonFormatter.DeserializationError += (e) => { deserializationErrors.Add(e.Message); };

                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var payment = GetPaymentUsingPayPal("sale");
                payment.transactions[0].related_resources.Clear();
                var createdPayment = payment.Create(apiContext);
                this.RecordConnectionDetails();

                // Verify no errors were encountered while deserializing the response.
                if (deserializationErrors.Any())
                {
                    Assert.Fail("Encountered errors while attempting to deserialize:" + Environment.NewLine + string.Join(Environment.NewLine, deserializationErrors));
                }

                // Verify the state of the response.
                Assert.AreEqual("created", createdPayment.state);
                Assert.IsTrue(createdPayment.id.StartsWith("PAY-"));
                Assert.IsTrue(!string.IsNullOrEmpty(createdPayment.token));

                // Verify the expected HATEOAS links: self, approval_url, & execute
                Assert.AreEqual(3, createdPayment.links.Count);
                Assert.IsNotNull(createdPayment.GetHateoasLink(BaseConstants.HateoasLinkRelations.Self));
                Assert.IsNotNull(createdPayment.GetHateoasLink(BaseConstants.HateoasLinkRelations.ApprovalUrl));
                Assert.IsNotNull(createdPayment.GetHateoasLink(BaseConstants.HateoasLinkRelations.Execute));
            }
            catch (ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Functional")]
        public void PaymentVerifyCreatePayPalPaymentForOrderResponse()
        {
            try
            {
                var deserializationErrors = new List<string>();
                JsonFormatter.DeserializationError += (e) => { deserializationErrors.Add(e.Message); };

                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var payment = GetPaymentUsingPayPal("order");
                var createdPayment = payment.Create(apiContext);
                this.RecordConnectionDetails();

                // Verify no errors were encountered while deserializing the response.
                if (deserializationErrors.Any())
                {
                    Assert.Fail("Encountered errors while attempting to deserialize:" + Environment.NewLine + string.Join(Environment.NewLine, deserializationErrors));
                }

                // Verify the state of the response.
                Assert.AreEqual("created", createdPayment.state);
                Assert.IsTrue(createdPayment.id.StartsWith("PAY-"));
                Assert.IsTrue(!string.IsNullOrEmpty(createdPayment.token));

                // Verify the expected HATEOAS links: self, approval_url, & execute
                Assert.AreEqual(3, createdPayment.links.Count);
                Assert.IsNotNull(createdPayment.GetHateoasLink(BaseConstants.HateoasLinkRelations.Self));
                Assert.IsNotNull(createdPayment.GetHateoasLink(BaseConstants.HateoasLinkRelations.ApprovalUrl));
                Assert.IsNotNull(createdPayment.GetHateoasLink(BaseConstants.HateoasLinkRelations.Execute));
            }
            catch (ConnectionException ex)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }

        [TestMethod, TestCategory("Functional")]
        public void PaymentVerifyCreateCreditCardPaymentForSaleResponse()
        {
            try
            {
                var deserializationErrors = new List<string>();
                JsonFormatter.DeserializationError += (e) => { deserializationErrors.Add(e.Message); };

                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var payment = GetPaymentUsingCreditCard("sale");
                payment.transactions[0].related_resources.Clear();
                var createdPayment = payment.Create(apiContext);
                this.RecordConnectionDetails();

                // Verify no errors were encountered while deserializing the response.
                if (deserializationErrors.Any())
                {
                    Assert.Fail("Encountered errors while attempting to deserialize:" + Environment.NewLine + string.Join(Environment.NewLine, deserializationErrors));
                }

                // Verify the state of the response.
                Assert.AreEqual("approved", createdPayment.state);
                Assert.IsTrue(createdPayment.id.StartsWith("PAY-"));
                Assert.IsTrue(string.IsNullOrEmpty(createdPayment.token));

                // Verify the expected HATEOAS links: self
                Assert.AreEqual(1, createdPayment.links.Count);
                Assert.IsNotNull(createdPayment.GetHateoasLink(BaseConstants.HateoasLinkRelations.Self));
            }
            catch (ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }
        #endregion
    }
}
