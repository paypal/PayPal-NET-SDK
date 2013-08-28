using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class CreditCardTokenTest
    {
        private CreditCardToken GetCreditCardToken()
        {
            CreditCardToken cardToken = new CreditCardToken();
            cardToken.credit_card_id = "CARD-8PV12506MG6587946KEBHH4A";
            cardToken.payer_id = "009";
            cardToken.expire_month = 10;
            cardToken.expire_year = 2015;
            return cardToken;
        }

        [TestMethod()]
        public void TestCreditCardToken()
        {
            CreditCardToken token = GetCreditCardToken();
            Assert.AreEqual(token.credit_card_id, "CARD-8PV12506MG6587946KEBHH4A");
            Assert.AreEqual(token.payer_id, "009");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            CreditCardToken token = GetCreditCardToken();
            string expected = "{\"credit_card_id\":\"CARD-8PV12506MG6587946KEBHH4A\",\"payer_id\":\"009\",\"expire_month\":10,\"expire_year\":2015}";
            string actual = token.ConvertToJson();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertToStringTest()
        {
            CreditCardToken token = GetCreditCardToken();
            Assert.IsFalse(token.ToString().Length == 0);
        }
    }
}
