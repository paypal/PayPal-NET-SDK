using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class ShippingAddressTest
    {
        private ShippingAddress CreateShippingAddress()
        {
            ShippingAddress shipping = new ShippingAddress();
            shipping.recipient_name = "PayPalUser";
            return shipping;
        }

        [TestMethod()]
        public void TestShippingAddress()
        {
            ShippingAddress shipping = CreateShippingAddress();
            Assert.AreEqual(shipping.recipient_name, "PayPalUser");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            ShippingAddress shipping = CreateShippingAddress();
            Assert.IsFalse(shipping.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            ShippingAddress shipping = CreateShippingAddress();
            Assert.IsFalse(shipping.ToString().Length == 0);
        }
    }
}
