using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;

namespace PayPal.Testing
{
    [TestClass]
    public class PresentationTest
    {
        public static readonly string PresentationJson = "{\"brand_name\": \"Test brand name\",\"logo_image\": \"http://www.paypal.com\",\"locale_code\": \"US\",\"return_url_label\":\"return\",\"note_to_seller_label\":\"thanks!\"}";

        public static Presentation GetPresentation()
        {
            return JsonFormatter.ConvertFromJson<Presentation>(PresentationJson);
        }

        [TestMethod, TestCategory("Unit")]
        public void PresentationObjectTest()
        {
            var presentation = GetPresentation();
            Assert.AreEqual("Test brand name", presentation.brand_name);
            Assert.AreEqual("http://www.paypal.com", presentation.logo_image);
            Assert.AreEqual("US", presentation.locale_code);
            Assert.AreEqual("return", presentation.return_url_label);
            Assert.AreEqual("thanks!", presentation.note_to_seller_label);
        }

        [TestMethod, TestCategory("Unit")]
        public void PresentationConvertToJsonTest()
        {
            Assert.IsFalse(GetPresentation().ConvertToJson().Length == 0);
        }

        [TestMethod, TestCategory("Unit")]
        public void PresentationToStringTest()
        {
            Assert.IsFalse(GetPresentation().ToString().Length == 0);
        }
    }
}
