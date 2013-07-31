using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PayPal;
using PayPal.Manager;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using PayPal.Api.Payments;

// ##Reauthorization Sample
// Sample showing how to do a reauthorization
namespace RestApiSample
{
    public partial class Reauthorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
            Authorization authorization = null;
            try
            {
                // ###AccessToken
                // Retrieve the access token from
                // OAuthTokenCredential by passing in
                // ClientID and ClientSecret
                // It is not mandatory to generate Access Token on a per call basis.
                // Typically the access token can be generated once and
                // reused within the expiry window
                string accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();

                // ###Reauthorization
                // Retrieve a authorization id from authorization object
                // by making a `Payment Using PayPal` with intent
                // as `authorize`. You can reauthorize a payment only once 4 to 29
                // days after 3-day honor period for the original authorization
                // expires.
                authorization = Authorization.Get(accessToken, "7GH53639GA425732B");
                Amount reauthorizeAmount = new Amount();
                reauthorizeAmount.currency = "USD";
                reauthorizeAmount.total = "1";
                authorization.amount = reauthorizeAmount;
                Authorization reauthorization = authorization.Reauthorize(accessToken);
                CurrContext.Items.Add("ResponseJson", JObject.Parse(reauthorization.ConvertToJson()).ToString(Formatting.Indented));

            }
            catch (PayPal.Exception.PayPalException ex)
            {
                CurrContext.Items.Add("Error", ex.Message);
            }
            CurrContext.Items.Add("RequestJson", JObject.Parse(authorization.ConvertToJson()).ToString(Formatting.Indented));
            Server.Transfer("~/Response.aspx");
        }
    }
}
