using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class ItemTest
    { 
        private Item GetItem()
        {
            Item itm = new Item();
            itm.name = "Item Name";
            itm.currency = "USD";
            itm.price = "10.50";
            itm.quantity = "5";
            itm.sku = "Sku";
            return itm;
        }

        [TestMethod()]
        public void TestItem()
        {
            Item itm = GetItem();
            Assert.AreEqual(itm.name, "Item Name");
            Assert.AreEqual(itm.currency, "USD");
            Assert.AreEqual(itm.price, "10.50");
            Assert.AreEqual(itm.quantity, "5");
            Assert.AreEqual(itm.sku, "Sku");
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            Item item = GetItem();
            Assert.IsFalse(item.ConvertToJson().Length == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Item item = GetItem();
            Assert.IsFalse(item.ToString().Length == 0);
        }
    }
}
