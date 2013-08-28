using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api.Payments;
using PayPal.Manager;
using PayPal;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class CaptureTest
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

        private Capture GetCapture()
        {
            Capture cap = new Capture();
            cap.amount = GetAmount();
            cap.create_time = "2013-01-15T15:10:05.123Z";
            cap.state = "Authorized";
            cap.parent_payment = "1000";
            cap.links = GetLinksList();
            cap.id = "001";
            return cap;
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

        private Payment GetPayment()
        {
            Payment pay = new Payment();
            pay.intent = "authorize";
            CreditCard card = GetCreditCard();
            List<FundingInstrument> instruments = new List<FundingInstrument>();
            FundingInstrument instrument = new FundingInstrument();
            instrument.credit_card = card;
            instruments.Add(instrument);
            Payer payer = new Payer();
            payer.payment_method = "credit_card";
            payer.funding_instruments = instruments;
            List<Transaction> transacts = new List<Transaction>();
            Transaction trans = new Transaction();
            trans.amount = GetAmount();
            transacts.Add(trans);
            pay.transactions = transacts;
            pay.payer = payer;
            return pay.Create(AccessToken);
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
        
        [TestMethod()]
        public void CaptureAmountTest()
        {
            Capture cap = GetCapture();
            Amount expected = GetAmount();
            Amount actual = cap.amount;
            Assert.AreEqual(expected.currency, actual.currency);
            Assert.AreEqual(expected.details.fee, actual.details.fee);
            Assert.AreEqual(expected.details.shipping, actual.details.shipping);
            Assert.AreEqual(expected.details.subtotal, actual.details.subtotal);
            Assert.AreEqual(expected.details.tax, actual.details.tax);
            Assert.AreEqual(expected.total, actual.total);
            Assert.AreEqual(cap.create_time, "2013-01-15T15:10:05.123Z");
            Assert.AreEqual("001", cap.id);
            Assert.AreEqual("1000", cap.parent_payment);
            Assert.AreEqual("Authorized", cap.state);
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Capture cap = GetCapture();            
            Assert.IsFalse(cap.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ConvertToStringTest()
        {
            Capture cap = GetCapture();
            Assert.IsFalse(cap.ToString().Length == 0);
        }

        [TestMethod()]
        public void CaptureIdTest()
        {
            Payment pay = GetPayment();
            string authorizationId = pay.transactions[0].related_resources[0].authorization.id;
            Authorization authorization = Authorization.Get(AccessToken, authorizationId);
            Capture cap = new Capture();
            Amount amt = new Amount();
            amt.total = "1";
            amt.currency = "USD";
            cap.amount = amt;
            Capture responseCapture = authorization.Capture(AccessToken, cap);
            Capture returnCapture = Capture.Get(AccessToken, responseCapture.id);
            Assert.AreEqual(responseCapture.id, returnCapture.id);
        }

        [TestMethod()]
        public void RefundCaptureTest()
        {
            Payment pay = GetPayment();
            string authorizationId = pay.transactions[0].related_resources[0].authorization.id;
            Authorization authorization = Authorization.Get(AccessToken, authorizationId);
            Capture cap = new Capture();
            Amount amnt = new Amount();
            amnt.total = "1";
            amnt.currency = "USD";
            cap.amount = amnt;
            Capture response = authorization.Capture(AccessToken, cap);
            Refund fund = new Refund();
            Amount refundAmount = new Amount();
            refundAmount.total = "1";
            refundAmount.currency = "USD";
            fund.amount = refundAmount;
            Refund responseRefund = response.Refund(AccessToken, fund);
            Assert.AreEqual("completed", responseRefund.state);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Value cannot be null. Parameter name: captureId cannot be null")]
        public void NullCaptureIdTest()
        {
            Capture cap = Capture.Get(AccessToken, null);  
        } 
    }
}
