using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;

namespace PayPal.Testing
{
    /// <summary>
    /// Summary description for InvoiceTest
    /// </summary>
    [TestClass]
    public class InvoiceTest : BaseTest
    {
        public static readonly string InvoiceJson =
            "{\"merchant_info\":" + MerchantInfoTest.MerchantInfoJson + "," +
            "\"billing_info\":[{\"email\":\"example@example.com\"}]," +
            "\"items\":[" + InvoiceItemTest.InvoiceItemJson + "]," +
            "\"note\":\"Medical Invoice 16 Jul, 2013 PST\"," +
            "\"allow_partial_payment\":true," +
            "\"payment_term\":{\"term_type\":\"NET_45\"}," +
            "\"minimum_amount_due\":" + CurrencyTest.CurrencyJson + "," + 
            "\"gratuity\":" + CurrencyTest.CurrencyJson + "," + 
            "\"shipping_info\":" + ShippingInfoTest.ShippingInfoJson + "}";

        public static Invoice GetInvoice()
        {
            return JsonFormatter.ConvertFromJson<Invoice>(InvoiceJson);
        }

        [TestMethod, TestCategory("Unit")]
        public void InvoiceObjectTest()
        {
            var testObject = GetInvoice();
            Assert.IsNotNull(testObject.merchant_info);
            Assert.IsNotNull(testObject.billing_info);
            Assert.IsTrue(testObject.billing_info.Count == 1);
            Assert.IsNotNull(testObject.items);
            Assert.IsTrue(testObject.items.Count == 1);
            Assert.IsNotNull(testObject.shipping_info);
            Assert.AreEqual("Medical Invoice 16 Jul, 2013 PST", testObject.note);
            Assert.IsNotNull(testObject.payment_term);
            Assert.AreEqual(testObject.allow_partial_payment, true);
            Assert.AreEqual(testObject.minimum_amount_due.value, CurrencyTest.GetCurrency().value);
            Assert.AreEqual(testObject.gratuity.value, CurrencyTest.GetCurrency().value);
        }

        [TestMethod, TestCategory("Unit")]
        public void InvoiceConvertToJsonTest()
        {
            Assert.IsFalse(GetInvoice().ConvertToJson().Length == 0);
        }

        [TestMethod, TestCategory("Unit")]
        public void InvoiceToStringTest()
        {
            Assert.IsFalse(GetInvoice().ToString().Length == 0);
        }

        [Ignore]
        public void InvoiceCreateTest()
        {
            try
            {
                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var invoice = GetInvoice();
                invoice.merchant_info.address.phone = null;
                invoice.shipping_info.address.phone = null;
                var createdInvoice = invoice.Create(apiContext);
                this.RecordConnectionDetails();

                Assert.IsNotNull(createdInvoice.id);
                Assert.AreEqual(invoice.note, createdInvoice.note);
            }
            catch(ConnectionException)
            {
                this.RecordConnectionDetails(false);
            }
        }

        [Ignore]
        public void InvoiceQrCodeTest()
        {
            try
            {
                var apiContext = TestingUtil.GetApiContext();
                this.RecordConnectionDetails();

                var invoice = GetInvoice();
                var createdInvoice = invoice.Create(apiContext);
                this.RecordConnectionDetails();

                var qrCode = Invoice.QrCode(apiContext, createdInvoice.id);
                this.RecordConnectionDetails();

                Assert.IsNotNull(qrCode);
                Assert.IsTrue(!string.IsNullOrEmpty(qrCode.image));

                createdInvoice.Delete(apiContext);
                this.RecordConnectionDetails();
            }
            catch (ConnectionException)
            {
                this.RecordConnectionDetails(false);
            }
        }
    }
}
