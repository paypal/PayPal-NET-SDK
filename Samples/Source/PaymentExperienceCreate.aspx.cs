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
using PayPal.Api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PayPal.Sample
{
    public partial class PaymentExperienceCreate : BaseSamplePage
    {
        protected override void RunSample()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();

            // Setup the profile we want to create
            var profile = new WebProfile()
            {
                name = Guid.NewGuid().ToString(),
                presentation = new Presentation()
                {
                    brand_name = "Sample brand",
                    locale_code = "US",
                    logo_image = "https://www.paypal.com/",
                    note_to_seller_label = "Thx",
                    return_url_label = "Retreat!"
                },
                input_fields = new InputFields()
                {
                    address_override = 1,
                    allow_note = true,
                    no_shipping = 0
                },
                flow_config = new FlowConfig()
                {
                    bank_txn_pending_url = "https://www.paypal.com/",
                    landing_page_type = "billing",
                    user_action = "commit",
                    return_uri_http_method = "GET"
                },
                temporary = true
            };

            #region Track Workflow
            //--------------------
            this.flow.AddNewRequest("Create profile", profile);
            //--------------------
            #endregion

            // Create the profile
            var response = profile.Create(apiContext);

            #region Track Workflow
            //--------------------
            this.flow.RecordResponse(response);
            //--------------------
            #endregion

            #region Cleanup
            // Cleanup by deleting the newly-created profile
            var retrievedProfile = WebProfile.Get(apiContext, response.id);
            retrievedProfile.Delete(apiContext);
            #endregion
        }
    }
}
