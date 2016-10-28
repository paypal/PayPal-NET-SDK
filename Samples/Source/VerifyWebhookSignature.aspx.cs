using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;

namespace PayPal.Sample
{
    public partial class VerifyWebhookSignature : BaseSamplePage
    {
        private static readonly String requestBody = "{\"id\":\"WH-9UG43882HX7271132-6E0871324L7949614\",\"event_version\":\"1.0\",\"create_time\":\"2016-09-21T22:00:45Z\",\"resource_type\":\"sale\",\"event_type\":\"PAYMENT.SALE.COMPLETED\",\"summary\":\"Payment completed for $ 21.0 USD\",\"resource\":{\"id\":\"80F85758S3080410K\",\"state\":\"completed\",\"amount\":{\"total\":\"21.00\",\"currency\":\"USD\",\"details\":{\"subtotal\":\"17.50\",\"tax\":\"1.30\",\"shipping\":\"2.20\"}},\"payment_mode\":\"INSTANT_TRANSFER\",\"protection_eligibility\":\"ELIGIBLE\",\"protection_eligibility_type\":\"ITEM_NOT_RECEIVED_ELIGIBLE,UNAUTHORIZED_PAYMENT_ELIGIBLE\",\"transaction_fee\":{\"value\":\"0.91\",\"currency\":\"USD\"},\"invoice_number\":\"57e3028db8d1b\",\"custom\":\"\",\"parent_payment\":\"PAY-7F371669SL612941HK7RQFDQ\",\"create_time\":\"2016-09-21T21:59:02Z\",\"update_time\":\"2016-09-21T22:00:06Z\",\"links\":[{\"href\":\"https://api.sandbox.paypal.com/v1/payments/sale/80F85758S3080410K\",\"rel\":\"self\",\"method\":\"GET\"},{\"href\":\"https://api.sandbox.paypal.com/v1/payments/sale/80F85758S3080410K/refund\",\"rel\":\"refund\",\"method\":\"POST\"},{\"href\":\"https://api.sandbox.paypal.com/v1/payments/payment/PAY-7F371669SL612941HK7RQFDQ\",\"rel\":\"parent_payment\",\"method\":\"GET\"}]},\"links\":[{\"href\":\"https://api.sandbox.paypal.com/v1/notifications/webhooks-events/WH-9UG43882HX7271132-6E0871324L7949614\",\"rel\":\"self\",\"method\":\"GET\"},{\"href\":\"https://api.sandbox.paypal.com/v1/notifications/webhooks-events/WH-9UG43882HX7271132-6E0871324L7949614/resend\",\"rel\":\"resend\",\"method\":\"POST\"}]}";

        private static readonly Dictionary<string, string> headers = new Dictionary<string, string>()
        {
            {"Paypal-Auth-Algo", "SHA256withRSA"},
            {"Paypal-Transmission-Id", "d938e770-8046-11e6-8103-6b62a8a99ac4"},
            {"Paypal-Cert-Url", "https://api.sandbox.paypal.com/v1/notifications/certs/CERT-360caa42-fca2a594-a5cafa77"},
            {"Paypal-Transmission-Sig", "eDOnWUj9FXOnr2naQnrdL7bhgejVSTwRbwbJ0kuk5wAtm2ZYkr7w5BSUDO7e5ZOsqLwN3sPn3RV85Jd9pjHuTlpuXDLYk+l5qiViPbaaC0tLV+8C/zbDjg2WCfvtf2NmFT8CHgPPQAByUqiiTY+RJZPPQC5np7j7WuxcegsJLeWStRAofsDLiSKrzYV3CKZYtNoNnRvYmSFMkYp/5vk4xGcQLeYNV1CC2PyqraZj8HGG6Y+KV4trhreV9VZDn+rPtLDZTbzUohie1LpEy31k2dg+1szpWaGYOz+MRb40U04oD7fD69vghCrDTYs5AsuFM2+WZtsMDmYGI0pxLjn2yw=="},
            {"Paypal-Transmission-Time", "2016-09-21T22:00:46Z"},
        };

        protected override void RunSample()
        {
            string accessToken = new OAuthTokenCredential("EBWKjlELKMYqRNQ6sYvFo64FtaRLRR5BdHEESmha49TM", "EO422dn3gQLgDbuwqTjzrFgFtaRLRR5BdHEESmha49TM", Configuration.GetConfig()).GetAccessToken();

            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = new APIContext(accessToken);

            var webhookId = "9XL90610J3647323C";

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.AddNewRequest("Verify received webhook event");
            #endregion

            // Construct a `VerifyWebhookSignature` and assign its properties from the headers received in your webhook event.
            var signatureVerification = new PayPal.Api.VerifyWebhookSignature
            {
                auth_algo = headers["Paypal-Auth-Algo"],
                cert_url = headers["Paypal-Cert-Url"],
                transmission_id = headers["Paypal-Transmission-Id"],
                transmission_sig = headers["Paypal-Transmission-Sig"],
                transmission_time = headers["Paypal-Transmission-Time"],
                webhook_id = webhookId,
                webhook_event = JsonFormatter.ConvertFromJson<WebhookEvent>(requestBody)
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.AddNewRequest("Verify the received webhook Event", signatureVerification);
            #endregion

            // Call the `Post` method on your `VerifyWebhookSignature` object to verify the Webhook event was actually sent by PayPal.
            var signatureVerificationResponse = signatureVerification.Post(apiContext);

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.RecordResponse(signatureVerificationResponse);
            #endregion
        }
    }
}
