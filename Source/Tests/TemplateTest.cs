using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Api.Tests
{
    [TestClass()]
    public class TemplateTest
    {
        public static readonly string TemplateJson =
            "{" +
"  \"name\": \"Hours Template\"," +
"  \"default\": true," +
"  \"unit_of_measure\": \"Hours\"," +
"  \"template_data\": {" +
"    \"items\": [" +
"      {" +
"        \"name\": \"Nutri Bullet\"," +
"        \"quantity\": 1," +
"        \"unit_price\": {" +
"          \"currency\": \"USD\"," +
"          \"value\": \"50.00\"" +
"        }" +
"}" +
"    ]," +
"    \"merchant_info\": {" +
"      \"email\": \"jaypatel512-facilitator@hotmail.com\"" +
"    }," +
"    \"tax_calculated_after_discount\": false," +
"    \"tax_inclusive\": false," +
"    \"note\": \"Thank you for your business.\"," +
"    \"logo_url\": \"https://pics.paypal.com/v1/images/redDot.jpeg\"" +
"  }," +
"  \"settings\": [" +
"    {" +
"      \"field_name\": \"items.date\"," +
"      \"display_preference\": {" +
"        \"hidden\": true" +
"      }" +
"    }," +
"    {" +
"      \"field_name\": \"custom\"," +
"      \"display_preference\": {" +
"        \"hidden\": true" +
"      }" +
"    }" +
"  ]" +
"}";

        public static InvoiceTemplate GetTemplate()
        {
            return JsonFormatter.ConvertFromJson<InvoiceTemplate>(TemplateJson);
        }

        [TestMethod, TestCategory("Unit")]
        public void TemplateObjectTest()
        {
            var template = GetTemplate();
            Assert.AreEqual(true, template.@default);
            Assert.AreEqual("Hours Template", template.name);
            Assert.AreEqual(1, template.template_data.items.Count);

            var template_data = template.template_data.items[0];
            Assert.AreEqual("Nutri Bullet", template_data.name);
            Assert.AreEqual(1, template_data.quantity);

            Assert.AreEqual(false, template.template_data.tax_calculated_after_discount);
            Assert.AreEqual(false, template.template_data.tax_inclusive);
            Assert.AreEqual("Thank you for your business.", template.template_data.note);

            Assert.AreEqual(2, template.settings.Count);
            var settings = template.settings[0];
            Assert.AreEqual("items.date", settings.field_name);
            Assert.AreEqual(true, settings.display_preference.hidden);

            settings = template.settings[1];
            Assert.AreEqual("custom", settings.field_name);
        }
    }
}
