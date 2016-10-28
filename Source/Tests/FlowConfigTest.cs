using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;

namespace PayPal.Testing
{
    [TestClass]
    public class FlowConfigTest
    {
        public static readonly string FlowConfigJson = "{\"landing_page_type\": \"billing\",\"bank_txn_pending_url\": \"http://www.paypal.com\",\"user_action\":\"commit\",\"return_uri_http_method\":\"GET\"}";

        public static FlowConfig GetFlowConfig()
        {
            return JsonFormatter.ConvertFromJson<FlowConfig>(FlowConfigJson);
        }

        [TestMethod, TestCategory("Unit")]
        public void FlowConfigObjectTest()
        {
            var flowConfig = GetFlowConfig();
            Assert.AreEqual("billing", flowConfig.landing_page_type);
            Assert.AreEqual("http://www.paypal.com", flowConfig.bank_txn_pending_url);
            Assert.AreEqual("commit", flowConfig.user_action);
            Assert.AreEqual("GET", flowConfig.return_uri_http_method);
        }

        [TestMethod, TestCategory("Unit")]
        public void FlowConfigConvertToJsonTest()
        {
            Assert.IsFalse(GetFlowConfig().ConvertToJson().Length == 0);
        }

        [TestMethod, TestCategory("Unit")]
        public void FlowConfigToStringTest()
        {
            Assert.IsFalse(GetFlowConfig().ToString().Length == 0);
        }
    }
}
