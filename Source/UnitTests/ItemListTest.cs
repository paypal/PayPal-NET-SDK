using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class ItemListTest
    {
        private ShippingAddress GetShippingAddress()
        {
            ShippingAddress shipping = new ShippingAddress();
            shipping.recipient_name = "PayPalUser";
            return shipping;
        }

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

        private ItemList GetItemList() 
        {
	        List<Item> items = new List<Item>();
	        items.Add(GetItem());
	        items.Add(GetItem());
	        ItemList itemList = new ItemList();
            itemList.items = items;
            itemList.shipping_address = GetShippingAddress();
	        return itemList;
	    }

	    [TestMethod()]
        public void TestItemList() 
        {
            ItemList list = GetItemList();
		    Assert.AreEqual(GetItemList().shipping_address.recipient_name, GetShippingAddress().recipient_name);
            Assert.AreEqual(list.items.Count, 2);
	    }

	    [TestMethod()]
        public void ConvertToJsonTest() 
        {
            ItemList list = GetItemList();
            Assert.IsFalse(list.ConvertToJson().Length == 0);
	    }

        [TestMethod()]
        public void ToStringTest() 
        {
            ItemList list = GetItemList();
            Assert.IsFalse(list.ToString().Length == 0);
	    }        
    }
}
