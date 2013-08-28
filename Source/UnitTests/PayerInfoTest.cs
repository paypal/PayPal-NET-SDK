using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class PayerInfoTest
    {
        private ShippingAddress GetShippingAddress()
        {
            ShippingAddress shipping = new ShippingAddress();
            shipping.recipient_name = "PayPalUser";
            return shipping;
        }

        private PayerInfo CreatePayerInfo()
        {
            PayerInfo info = new PayerInfo();
            info.first_name = "Joe";
            info.last_name = "Shopper";
            info.email = "Joe.Shopper@email.com";
            info.payer_id = "100";
            info.phone = "12345";
            info.shipping_address = GetShippingAddress();
            return info;
        }
        
        [TestMethod()]
        public void ConvertToJsonTest()
        {
            PayerInfo info = CreatePayerInfo();
            Assert.IsFalse(info.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            PayerInfo info = CreatePayerInfo();
            Assert.IsFalse(info.ToString().Length == 0);
        }
    }
}
