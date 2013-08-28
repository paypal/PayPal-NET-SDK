using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class AddressTest
    {
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
        public void AddressObjectTest()
        {
            Address add = GetAddress();
            Assert.AreEqual("2211", add.line1);
            Assert.AreEqual("N 1st St", add.line2);
            Assert.AreEqual("San Jose", add.city);
            Assert.AreEqual("California", add.state);
            Assert.AreEqual("95131", add.postal_code);
            Assert.AreEqual("US", add.country_code);
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Address add = GetAddress();
            Assert.IsFalse(add.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Address add = GetAddress();
            Assert.IsFalse(add.ToString().Length == 0);
        }
    }
}
