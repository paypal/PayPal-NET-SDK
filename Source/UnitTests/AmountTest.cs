using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class AmountTest
    {
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
            amt.total = "100";
            amt.details = GetDetails();            
            return amt;
        }
        
        [TestMethod()]
        public void TestAmount()
        {
            Amount amt = GetAmount();            
            Assert.AreEqual("USD", amt.currency);
            Assert.AreEqual("100", amt.total);
            Assert.AreEqual("75", amt.details.subtotal);
            Assert.AreEqual("15", amt.details.tax);
            Assert.AreEqual("2", amt.details.fee);
            Assert.AreEqual("10", amt.details.shipping);
            Assert.AreEqual("75", amt.details.subtotal);
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Amount amt = GetAmount();
            Assert.IsFalse(amt.ConvertToJson().Length == 0);
        }
        
        [TestMethod()]
        public void ToStringTest()
        {
            Amount amt = GetAmount();
            Assert.IsFalse(amt.ToString().Length == 0);
        }
    }
}
