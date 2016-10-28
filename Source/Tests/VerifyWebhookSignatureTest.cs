using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;

namespace PayPal.Testing
{
    [TestClass]
    public class VerifyWebhookSignatureTest
    {
        public static readonly string VerifyWebhookSignatureJson =
            "{\"auth_algo\":\"TestSample\"," +
            "\"cert_url\":\"http://www.google.com\"," +
            "\"transmission_id\":\"TestSample\"," +
            "\"transmission_sig\":\"TestSample\"," +
            "\"transmission_time\":\"TestSample\"," +
            "\"webhook_id\":\"TestSample\"," +
            "\"webhook_event\":" + WebhookEventTest.WebhookEventJson + "}";


        public static VerifyWebhookSignature GetVerifyWebhookSignature()
        {
            return JsonFormatter.ConvertFromJson<VerifyWebhookSignature>(VerifyWebhookSignatureJson);
        }

        [TestMethod, TestCategory("Unit")]
        public void VerifyWebhookSignatureObjectTest()
        {
            var testObject = GetVerifyWebhookSignature();
            Assert.AreEqual("TestSample", testObject.auth_algo);
            Assert.AreEqual("http://www.google.com", testObject.cert_url);
            Assert.AreEqual("TestSample", testObject.transmission_id);
            Assert.AreEqual("TestSample", testObject.transmission_sig);
            Assert.AreEqual("TestSample", testObject.transmission_time);
            Assert.AreEqual("TestSample", testObject.webhook_id);
            Assert.AreEqual(WebhookEventTest.WebhookEventJson, JsonFormatter.ConvertToJson(testObject.webhook_event));
        }
    }
}
