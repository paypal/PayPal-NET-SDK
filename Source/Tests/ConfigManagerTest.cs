using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PayPal.Api;
using PayPal;

namespace PayPal.Testing
{
    [TestClass]
    public class ConfigManagerTest
    {
        [TestMethod, TestCategory("Unit")]
        public void LoadConfigDefaults()
        {
            var config = ConfigManager.GetConfigWithDefaults(null);
            Assert.IsNotNull(config);
            Assert.AreEqual("30000", config[BaseConstants.HttpConnectionTimeoutConfig]);
            Assert.AreEqual("3", config[BaseConstants.HttpConnectionRetryConfig]);
            Assert.AreEqual("sandbox", config[BaseConstants.ApplicationModeConfig]);
        }

        [TestMethod, TestCategory("Unit")]
        public void LoadConfigFromAppConfig()
        {
            var config = ConfigManager.Instance.GetProperties();
            Assert.IsNotNull(config);
            Assert.AreEqual("sandbox", config[BaseConstants.ApplicationModeConfig]);
            Assert.AreEqual("360000", config[BaseConstants.HttpConnectionTimeoutConfig]);
            Assert.AreEqual("3", config[BaseConstants.HttpConnectionRetryConfig]);
            Assert.IsNotNull(config[BaseConstants.ClientId]);
            Assert.IsNotNull(config[BaseConstants.ClientSecret]);
        }

        [TestMethod, TestCategory("Unit")]
        public void VerifyIsLiveModeEnabledWithDefaultConfig()
        {
            var config = ConfigManager.GetConfigWithDefaults(null);
            Assert.IsFalse(ConfigManager.IsLiveModeEnabled(config));
        }

        [TestMethod, TestCategory("Unit")]
        public void VerifyIsLiveModeEnabledWithSandboxModeSet()
        {
            var config = new Dictionary<string, string>
            {
                { BaseConstants.ApplicationModeConfig, BaseConstants.SandboxMode }
            };
            Assert.IsFalse(ConfigManager.IsLiveModeEnabled(config));
        }

        [TestMethod, TestCategory("Unit")]
        public void VerifyIsLiveModeEnabledWithLiveModeSet()
        {
            var config = new Dictionary<string, string>
            {
                { BaseConstants.ApplicationModeConfig, BaseConstants.LiveMode }
            };
            Assert.IsTrue(ConfigManager.IsLiveModeEnabled(config));
        }
    }
}
