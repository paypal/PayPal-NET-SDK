using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class PayeeTest
    {
        private Payee GetPayee()
        {
            Payee pay = new Payee();
            pay.merchant_id = "100";
            pay.email = "paypaluser@email.com";
            pay.phone = "716-298-1822";
            return pay;
        }

        [TestMethod()]
        public void PayeeObjectTest()
        {
            Payee pay = GetPayee();
            Assert.AreEqual(pay.merchant_id, "100");
            Assert.AreEqual(pay.email, "paypaluser@email.com");
            Assert.AreEqual(pay.phone, "716-298-1822");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Payee pay = GetPayee();
            Assert.IsFalse(pay.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Payee pay = GetPayee();
            Assert.IsFalse(pay.ToString().Length == 0);
        }
    }
}
