using System;
using System.Linq;
using System.Collections.Generic;
using PayPal.Sample.Utilities;

namespace PayPal.Sample
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<SampleCategory> Categories { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the categories if this is the first time loading.
            if (this.Categories == null)
            {
                this.Categories = new List<SampleCategory>
                {
                    new SampleCategory
                    {
                        Title = "Payments",
                        Id = "payments",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Make a payment with a PayPal account", ExecutePage = "PaymentWithPayPal.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Make a payment with a PayPal account and include a discount", ExecutePage = "PaymentWithPayPalWithDiscount.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Make a payment with a credit card", ExecutePage = "PaymentWithCreditCard.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Make a payment with a stored credit card", ExecutePage = "PaymentWithSavedCard.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a payment", ExecutePage = "GetPayment.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a history of payments", ExecutePage = "GetPaymentHistory.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a sale transaction (completed payment)", ExecutePage = "GetSale.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Refund a sale", ExecutePage = "SaleRefund.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a refund", ExecutePage = "GetRefund.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Create and process an order", ExecutePage = "OrderSample.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of an authorized payment", ExecutePage = "GetAuthorization.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Capture an authorized payment", ExecutePage = "AuthorizationCapture.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Void an authorized payment", ExecutePage = "AuthorizationVoid.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Reauthorize a payment", ExecutePage = "Reauthorization.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a captured payment", ExecutePage = "GetCapture.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Refund a captured payment", ExecutePage = "RefundCapture.aspx", HasSourcePage = true }
                        }
                    },
                    new SampleCategory
                    {
                        Title = "Vault",
                        Id = "vault",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Create and store a credit card", ExecutePage = "CreateCreditCard.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a credit card", ExecutePage = "GetCreditCard.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a list of credit cards", ExecutePage = "CreditCardList.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete a credit card", ExecutePage = "DeleteCreditCard.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Update a credit card", ExecutePage = "CreditCardUpdate.aspx", HasSourcePage = true }
                        }
                    },
                    new SampleCategory
                    {
                        Title = "Web Experience Profiles for Payments",
                        Id = "experience",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Create a new web experience profile", ExecutePage = "PaymentExperienceCreate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a web experience profile", ExecutePage = "PaymentExperienceGet.aspx", HasSourcePage = true },
                            new SampleItem { Title = "List web experience profiles", ExecutePage = "PaymentExperienceGetList.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Update a web experience profile", ExecutePage = "PaymentExperienceUpdate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Partially update a web experience profile", ExecutePage = "PaymentExperiencePartialUpdate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete a web experience profile", ExecutePage = "PaymentExperienceDelete.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Make a PayPal payment using a web experience profile", ExecutePage = "PaymentWithPayPalWithWebExperienceProfile.aspx", HasSourcePage = true }
                        }
                    },
                    new SampleCategory
                    {
                        Title = "Billing Plans &amp; Agreements",
                        Id = "billing",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Create a billing plan", ExecutePage = "BillingPlanCreate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Update a billing plan", ExecutePage = "BillingPlanUpdate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete a billing plan", ExecutePage = "BillingPlanDelete.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a billing plan", ExecutePage = "BillingPlanGet.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a list of billing plans", ExecutePage = "BillingPlanGetList.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Setup a billing agreement using a PayPal account", ExecutePage = "BillingAgreementCreateAndExecute.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Setup a billing agreement using a credit card", ExecutePage = "BillingAgreementWithCreditCard.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a billing agreement", ExecutePage = "BillingAgreementGet.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a list of transactions for a billing agreement", ExecutePage = "BillingAgreementListTransactions.aspx", HasSourcePage = true }
                        }
                    },
                    new SampleCategory
                    {
                        Title = "Invoicing",
                        Id = "invoicing",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Create an invoice", ExecutePage = "InvoiceCreate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Send an invoice", ExecutePage = "InvoiceSend.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Update an invoice", ExecutePage = "InvoiceUpdate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve an invoice", ExecutePage = "InvoiceGet.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Get invoices of a merchant", ExecutePage = "InvoiceGetList.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Search for invoices", ExecutePage = "InvoiceSearch.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Send an invoice reminder", ExecutePage = "InvoiceSendReminder.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Cancel an invoice", ExecutePage = "InvoiceCancel.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete an invoice", ExecutePage = "InvoiceDelete.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a QR code", ExecutePage = "InvoiceGetQrCode.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Record a payment", ExecutePage = "InvoiceRecordPayment.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete a payment", ExecutePage = "InvoiceDeletePayment.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Record a refund", ExecutePage = "InvoiceRecordRefund.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete a refund", ExecutePage = "InvoiceDeleteRefund.aspx", HasSourcePage = true }
                        }
                    },
                    new SampleCategory
                    {
                        Title = "Invoice Templates",
                        Id = "invoice-templates",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Create an invoice template", ExecutePage = "InvoiceTemplateCreate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete an invoice template", ExecutePage = "InvoiceTemplateDelete.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve an invoice template", ExecutePage = "InvoiceTemplateRetrieve.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retreive a list of invoice templates", ExecutePage = "InvoiceTemplateRetrieveList.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Update an invoice template", ExecutePage = "InvoiceTemplateUpdate.aspx", HasSourcePage = true },
                        }
                    },
                    new SampleCategory
                    {
                        Title = "Webhooks",
                        Id = "webhooks",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Create and retrieve a webhook", ExecutePage = "WebhookCreate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a list of webhooks", ExecutePage = "WebhookGetAll.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve a list of recent webhook events", ExecutePage = "WebhookEventList.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Update a webhook", ExecutePage = "WebhookUpdate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete a webhook", ExecutePage = "WebhookDelete.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Delete all webhooks", ExecutePage = "WebhookDelete.aspx?deleteAll=true", Note = "Use this if you get the error WEBHOOK_NUMBER_LIMIT_EXCEEDED" },
                            new SampleItem { Title = "Verify a Webhook Event", ExecutePage = "VerifyWebhookSignature.aspx", HasSourcePage = true }
                        }
                    },
                    new SampleCategory
                    {
                        Title = "Payouts",
                        Id = "payouts",
                        Items = new List<SampleItem>
                        {
                            new SampleItem { Title = "Create a payout", ExecutePage = "PayoutCreate.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a payout", ExecutePage = "PayoutGet.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Retrieve the details of a payout item", ExecutePage = "PayoutItemGet.aspx", HasSourcePage = true },
                            new SampleItem { Title = "Cancel a payout item", ExecutePage = "PayoutItemCancel.aspx", HasSourcePage = true }
                        }
                    }
                };
            }
        }
    }
}
