using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class RedirectUrlsTest
    {
        private RedirectUrls CreateRedirectUrls()
        {
            RedirectUrls urls = new RedirectUrls();
            urls.cancel_url = "http://ebay.com/";
            urls.return_url = "http://paypal.com/";
            return urls;
        }

        [TestMethod()]
        public void TestRedirectUrls()
        {
            RedirectUrls urls = CreateRedirectUrls();
            Assert.AreEqual(urls.cancel_url, "http://ebay.com/");
            Assert.AreEqual(urls.return_url, "http://paypal.com/");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            RedirectUrls urls = CreateRedirectUrls();
            Assert.IsFalse(urls.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            RedirectUrls urls = CreateRedirectUrls();
            Assert.IsFalse(urls.ToString().Length == 0);
        }
    }
}
