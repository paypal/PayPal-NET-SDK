// #GetAuthorization Sample
// This sample code demonstrates how to 
// retrieve an Authorization resource
// API used: GET /v1/payments/authorization/{id}
using System;
using System.Web;
using PayPal.Api;
using PayPal;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PayPal.Sample
{
    public partial class GetAuthorization : BaseSamplePage
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
            var authorizationId = "<your authorization id here>";

            #region Track Workflow
            //--------------------
            this.flow.AddNewRequest("Get authorization details", description: "ID: " + authorizationId);
            //--------------------
            #endregion

            // Get Authorization by sending
            // a GET request with authorization Id
            // to the
            // URI v1/payments/authorization/{id}
            var authorization = Authorization.Get(apiContext, authorizationId);

            #region Track Workflow
            //--------------------
            this.flow.RecordResponse(authorization);
            //--------------------
            #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }

    }
}
