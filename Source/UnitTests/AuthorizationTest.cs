using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api.Payments;
using PayPal.Manager;
using PayPal;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class AuthorizationTest
    {
        private string ClientId
        {
            get
            {
                string Id = PayPal.Manager.ConfigManager.Instance.GetProperties()["ClientID"];
                return Id;
            }
        }

        private string ClientSecret
        {
            get
            {
                string secret = ConfigManager.Instance.GetProperties()["ClientSecret"];
                return secret;
            }
        }

        private string AccessToken
        {
            get
            {
                string token = new OAuthTokenCredential(ClientId, ClientSecret).GetAccessToken();
                return token;
            }
        }

        private List<Links> GetLinksList()
        {
            Links lnk = new Links();
            lnk.href = "http://www.paypal.com";
            lnk.method = "POST";
            lnk.rel = "authorize";
            List<Links> lnks = new List<Links>();
            lnks.Add(lnk);
            return lnks;
        }

        private Details GetDetails()
        {
            Details detail = new Details();
            detail.tax = "15";
            detail.fee = "2";
            detail.shipping = "10";
            detail.subtotal = "75";
            return detail;
        }

        private Amount GetAmount()
        {
            Amount amt = new Amount();
            amt.currency = "USD";
            amt.details = GetDetails();
            amt.total = "100";
            return amt;
        }

        private Authorization GetAuthorization()
        {
            Authorization authorize = new Authorization();
            authorize.amount = GetAmount();
            authorize.create_time = "2013-01-15T15:10:05.123Z";
            authorize.id = "007";
            authorize.parent_payment = "1000";
            authorize.state = "Authorized";
            authorize.links = GetLinksList();
            return authorize;
        }

        private Address GetAddress()
        {
            Address add = new Address();
            add.line1 = "2211";
            add.line2 = "N 1st St";
            add.city = "San Jose";
            add.phone = "408-456-0392";
            add.postal_code = "95131";
            add.state = "California";
            add.country_code = "US";
            return add;
        }

        private CreditCard GetCreditCard()
        {
            CreditCard card = new CreditCard();
            card.cvv2 = "962";
            card.expire_month = 01;
            card.expire_year = 2015;
            card.first_name = "John";
            card.last_name = "Doe";
            card.number = "4825854086744369";
            card.type = "visa";
            card.state = "New York";
            card.payer_id = "008";
            card.id = "002";
            card.billing_address = GetAddress();
            return card;
        } 

        private Payment CreatePayment()
        {
            Payment pay = new Payment();
            pay.intent = "authorize";
            CreditCard card = GetCreditCard();
            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            FundingInstrument instrument = new FundingInstrument();
            instrument.credit_card = card;
            fundingInstrumentList.Add(instrument);
            Payer payr = new Payer();
            payr.payment_method = "credit_card";
            payr.funding_instruments = fundingInstrumentList;
            List<Transaction> transactionList = new List<Transaction>();
            Transaction trans = new Transaction();
            trans.amount = GetAmount();
            transactionList.Add(trans);
            pay.transactions = transactionList;
            pay.payer = payr;
            return pay.Create(AccessToken);
        }     

        [TestMethod()]
        public void TestAuthorization()
        {
            Authorization authorize = GetAuthorization();
            Amount expected = GetAmount();
            Amount actual = authorize.amount;
            Assert.AreEqual(expected.currency, actual.currency);
            Assert.AreEqual(expected.details.fee, actual.details.fee);
            Assert.AreEqual(expected.details.shipping, actual.details.shipping);
            Assert.AreEqual(expected.details.subtotal, actual.details.subtotal);
            Assert.AreEqual(expected.details.tax, actual.details.tax);
            Assert.AreEqual(expected.total, actual.total);
            Assert.AreEqual(authorize.id, "007");
            Assert.AreEqual(authorize.create_time, "2013-01-15T15:10:05.123Z");
            Assert.AreEqual(authorize.parent_payment, "1000");
            Assert.AreEqual(authorize.state, "Authorized");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Authorization authorize = GetAuthorization();
            Assert.IsFalse(authorize.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Authorization authorize = GetAuthorization();
            Assert.IsFalse(authorize.ToString().Length == 0);
        }

        [TestMethod()]
        public void AuthorizationGetTest()
        {
            Payment pay = CreatePayment();
            string authorizationId = pay.transactions[0].related_resources[0].authorization.id;
            Authorization authorize = Authorization.Get(AccessToken, authorizationId);
            Assert.AreEqual(authorizationId, authorize.id);
        }

        [TestMethod()]
        public void AuthorizationCaptureTest()
        {
            Payment pay = CreatePayment();
            string authorizationId = pay.transactions[0].related_resources[0].authorization.id;
            Authorization authorize = Authorization.Get(AccessToken, authorizationId);
            Capture cap = new Capture();
            Amount amt = new Amount();
            amt.total = "1";
            amt.currency = "USD";
            cap.amount = amt;
            Capture response = authorize.Capture(AccessToken, cap);
            Assert.AreEqual("completed", response.state);
        }

        [TestMethod()]
        public void AuthorizationVoidTest()
        {
            Payment pay = CreatePayment();
            string authorizationId = pay.transactions[0].related_resources[0].authorization.id;
            Authorization authorize = Authorization.Get(AccessToken, authorizationId);
            Authorization authorizationResponse = authorize.Void(AccessToken);
            Assert.AreEqual("voided", authorizationResponse.state);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Value cannot be null. Parameter name: AccessToken cannot be null")]
        public void NullAccessTokenTest()
        {
            string token = null;
            Payment pay = CreatePayment();
            string authorizationId = pay.transactions[0].related_resources[0].authorization.id;
            Authorization authorization = Authorization.Get(token, authorizationId);            
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Value cannot be null. Parameter name: AccessToken cannot be null")]
        public void NullAuthorizationIdTest()
        {
            string authorizationId = null;
            Authorization authorization = Authorization.Get(AccessToken, authorizationId);
        }
    }
}
