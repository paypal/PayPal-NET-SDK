using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;

namespace PayPal.Testing
{
    [TestClass]
    public class APIContextTest
    {
        [TestMethod, TestCategory("Unit")]
        public void APIContextValidConstructorTest()
        {
            var apiContext = new APIContext();
            Assert.IsTrue(string.IsNullOrEmpty(apiContext.RequestId));
            Assert.IsFalse(apiContext.MaskRequestId);
            Assert.IsTrue(string.IsNullOrEmpty(apiContext.AccessToken));
            Assert.IsNull(apiContext.Config);
            Assert.IsNull(apiContext.HTTPHeaders);
            Assert.IsNotNull(apiContext.SdkVersion);
        }

        [TestMethod, TestCategory("Unit")]
        public void APIContextValidConstructorWithAccessTokenTest()
        {
            var apiContext = new APIContext("abc");
            Assert.IsTrue(string.IsNullOrEmpty(apiContext.RequestId));
            Assert.IsFalse(apiContext.MaskRequestId);
            Assert.AreEqual("abc", apiContext.AccessToken);
            Assert.IsNull(apiContext.Config);
            Assert.IsNull(apiContext.HTTPHeaders);
            Assert.IsNotNull(apiContext.SdkVersion);
        }

        [TestMethod, TestCategory("Unit")]
        public void APIContextValidConstructorWithAccessTokenAndRequestIdTest()
        {
            var apiContext = new APIContext("abc", "xyz");
            Assert.AreEqual("xyz", apiContext.RequestId);
            Assert.IsFalse(apiContext.MaskRequestId);
            Assert.AreEqual("abc", apiContext.AccessToken);
            Assert.IsNull(apiContext.Config);
            Assert.IsNull(apiContext.HTTPHeaders);
            Assert.IsNotNull(apiContext.SdkVersion);
        }

        [TestMethod, TestCategory("Unit")]
        public void APIContextInvalidAccessTokenConstructorTest()
        {
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => new APIContext(""));
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => new APIContext("", "xyz"));
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => new APIContext(null));
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => new APIContext(null, "xyz"));
        }

        [TestMethod, TestCategory("Unit")]
        public void APIContextInvalidRequestIdConstructorTest()
        {
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => new APIContext("abc", ""));
            TestingUtil.AssertThrownException<System.ArgumentNullException>(() => new APIContext("abc", null));
        }

        [TestMethod, TestCategory("Unit")]
        public void APIContextResetRequestIdTest()
        {
            var apiContext = new APIContext();
            var originalRequestId = apiContext.RequestId;
            apiContext.ResetRequestId();
            Assert.AreNotEqual(originalRequestId, apiContext.RequestId);
        }
    }
}
