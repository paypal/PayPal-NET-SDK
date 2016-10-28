using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;
using System.Collections.Generic;
using System.Text;

namespace PayPal.Testing
{
    [TestClass]
    public class UserInfoTest : BaseTest
    {
        [TestMethod, TestCategory("Functional"), Ignore]
        public void UserInfoGetUserInfoWithRefreshTokenTest()
        {
            try
            {
                var config = ConfigManager.Instance.GetProperties();
                config[BaseConstants.ClientId] = "AYSq3RDGsmBLJE-otTkBtM-jBRd1TCQwFf9RGfwddNXWz0uFU9ztymylOhRS";
                config[BaseConstants.ClientSecret] = "EGnHDxD_qRPdaLdZz8iCr8N7_MzF-YHPTkjs6NKYQvQSBngp4PTTVWkPZRbL";
                var apiContext = new APIContext() { Config = config };

                // Using the refresh token, first get an access token.
                var tokenInfo = new Tokeninfo();
                tokenInfo.refresh_token = "W1JmxG-Cogm-4aSc5Vlen37XaQTj74aQcQiTtXax5UgY7M_AJ--kLX8xNVk8LtCpmueFfcYlRK6UgQLJ-XHsxpw6kZzPpKKccRQeC4z2ldTMfXdIWajZ6CHuebs";
                var refreshTokenParameters = new CreateFromRefreshTokenParameters();
                var token = tokenInfo.CreateFromRefreshToken(apiContext, refreshTokenParameters);
                this.RecordConnectionDetails();

                var userInfoParameters = new UserinfoParameters();
                userInfoParameters.SetAccessToken(token.access_token);
                    
                // Get the user information.
                var userInfo = PayPal.Api.OpenIdConnect.Userinfo.GetUserinfo(userInfoParameters);
                this.RecordConnectionDetails();

                Assert.AreEqual("account", userInfo.family_name);
                Assert.AreEqual("facilitator account", userInfo.name);
                Assert.AreEqual("facilitator", userInfo.given_name);
                Assert.AreEqual("BUSINESS", userInfo.account_type);
                Assert.AreEqual("https://www.paypal.com/webapps/auth/identity/user/jWZav5QbA94DNm5FzsNOsq88y4QYrRvu4KLfUydcJqU", userInfo.user_id);
                Assert.IsTrue(userInfo.verified_account.Value);
                Assert.AreEqual("en_US", userInfo.locale);
            }
            catch(ConnectionException)
            {
                this.RecordConnectionDetails(false);
                throw;
            }
        }
    }
}
