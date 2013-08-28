using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class LinksTest
    {
        private Links GetLinks()
        {
            Links link = new Links();
            link.href = "http://paypal.com/";
            link.method = "POST";
            link.rel = "authorize";
            return link;
        }

        [TestMethod()]
        public void TestLinks()
        {
            Links link = GetLinks();
            Assert.AreEqual(link.href, "http://paypal.com/");
            Assert.AreEqual(link.method, "POST");
            Assert.AreEqual(link.rel, "authorize");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Links link = GetLinks();
            Assert.IsFalse(link.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Links link = GetLinks();
            Assert.IsFalse(link.ToString().Length == 0);
        } 
    }
}
