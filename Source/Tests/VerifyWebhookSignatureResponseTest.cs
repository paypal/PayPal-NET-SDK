using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;

namespace PayPal.Testing
{
    [TestClass]
    public class VerifyWebhookSignatureResponseTest
    {
        public static readonly string VerifyWebhookSignatureResponseJson =
            "{\"verification_status\":\"TestSample\"}";


        public static VerifyWebhookSignatureResponse GetVerifyWebhookResponseSignature()
        {
            return JsonFormatter.ConvertFromJson<VerifyWebhookSignatureResponse>(VerifyWebhookSignatureResponseJson);
        }

        [TestMethod, TestCategory("Unit")]
        public void VerifyWebhookSignatureObjectTest()
        {
            var testObject = GetVerifyWebhookResponseSignature();
            Assert.AreEqual("TestSample", testObject.verification_status);
        }
    }
}
