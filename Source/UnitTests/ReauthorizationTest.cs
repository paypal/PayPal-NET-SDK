using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api.Payments;
using PayPal.Manager;
using PayPal;

namespace RestApiSDKUnitTest
{
    [TestClass]
    public class ReauthorizationTest
    {  
        private string ClientId
        {
            get
            {
                string Id = PayPal.Manager.ConfigManager.Instance.GetProperties()["ClientID"];
                return Id;
            }
        }

        private string ClientSecret
        {
            get
            {
                string secret = ConfigManager.Instance.GetProperties()["ClientSecret"];
                return secret;
            }
        }

        private string AccessToken
        {
            get
            {
                string token = new OAuthTokenCredential(ClientId, ClientSecret).GetAccessToken();
                return token;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(PayPal.Exception.PayPalException), "Exception in HttpConnection Execute: Invalid HTTP response The remote server returned an error: (400) Bad Request.")]
        public void TestReauthorization()
        {
            Authorization authorization = Authorization.Get(AccessToken, "7GH53639GA425732B");
            Amount reauthorizeAmount = new Amount();
            reauthorizeAmount.currency = "USD";
            reauthorizeAmount.total = "1";
            authorization.amount = reauthorizeAmount;
            Authorization reauthorization = authorization.Reauthorize(AccessToken);          
        }
    }
}
