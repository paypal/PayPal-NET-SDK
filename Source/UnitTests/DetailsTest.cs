using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class DetailsTest
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

        [TestMethod()]
        public void TestDetails()
        {
            Details detail = GetDetails();
            Assert.AreEqual("75", detail.subtotal);
            Assert.AreEqual("15", detail.tax);
            Assert.AreEqual("10", detail.shipping);
            Assert.AreEqual("2", detail.fee);
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Details detail = GetDetails();
            Assert.IsFalse(detail.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Details detail = GetDetails();
            Assert.IsFalse(detail.ToString().Length == 0);
        }
    }
}
