// # Create an invoice
// Creates an invoice in a draft state. After you create an invoice that includes an `items` array, you can then send the invoice.
// > Note: The merchant specified in an invoice must have a PayPal account in good standing.
using PayPal.Api;
using System;
using System.Collections.Generic;

namespace PayPal.Sample
{
    public partial class InvoiceTemplateDelete : BaseSamplePage
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

            var invoiceTemplate = new InvoiceTemplate()
            {
                name = "Template " + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8),
                @default = true,
                unit_of_measure = "HOURS",
                template_data = new InvoiceTemplateData()
                {
                    items = new List<InvoiceItem>()
                    {
                        new InvoiceItem()
                        {
                            name = "Nutri Bullet",
                            quantity = 1.0F,
                            unit_price = new Currency()
                            {
                                currency = "USD",
                                value = "50.00"
                            }
                        }
                    },
                    merchant_info = new MerchantInfo()
                    {
                        email = "jziaja.test.merchant-facilitator@gmail.com"
                    },
                    tax_calculated_after_discount = false,
                    tax_inclusive = false,
                    note = "Thank you for your business",
                    logo_url = "https://pics.paypal.com/v1/images/redDot.jpeg",
                },
                settings = new List<InvoiceTemplateSettings>()
                {
                    new InvoiceTemplateSettings()
                    {
                        field_name = "items.date",
                        display_preference = new InvoiceTemplateSettingsMetadata()
                        {
                            hidden = true
                        }
                    },
                    new InvoiceTemplateSettings()
                    {
                        field_name = "custom",
                        display_preference = new InvoiceTemplateSettingsMetadata()
                        {
                            hidden = true
                        }
                    }
                }
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.AddNewRequest("Create the template.", invoiceTemplate);
            #endregion

            // Create the invoice template
            var createdTemplate = invoiceTemplate.Create(apiContext);

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.RecordResponse(createdTemplate);
            #endregion

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.AddNewRequest("Delete the template.");
            #endregion

            // Delete the invoice template
            createdTemplate.Delete(apiContext);

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.RecordActionSuccess("Template deleted successfully.");
            #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }
    }
}
