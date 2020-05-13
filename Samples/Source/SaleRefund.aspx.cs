// #SaleRefund Sample
// This sample code demonstrate how you can 
// process a refund on a sale transaction created 
// using the Payments API.
// API used: /v1/payments/sale/{sale-id}/refund
using PayPal.Api;
using PayPal.Sample.Utilities;
using System;
using System.Collections.Generic;

namespace PayPal.Sample
{
    public partial class SaleRefund : BaseSamplePage
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

            string payerId = Request.Params["PayerID"];
            if (string.IsNullOrEmpty(payerId))
            {
                // ###Redirect URLS
                // These URLs will determine how the user is redirected from PayPal once they have either approved or canceled the payment.
                var baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/SaleRefund.aspx?";
                var guid = Convert.ToString((new Random()).Next(100000));
                var redirectUrl = baseURI + "guid=" + guid;

                // ###Payment
                // A Payment Resource; create one using
                // the above types and intent as `sale` or `authorize`
                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer
                    {
                        payment_method = "paypal"
                    },
                    transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            description = "Transaction description.",
                            invoice_number = Common.GetRandomInvoiceNumber(),
                            amount = new Amount
                            {
                                currency = "USD",
                                total = "100.00",
                                details = new Details
                                {
                                    tax = "15",
                                    shipping = "10",
                                    subtotal = "75"
                                }
                            },
                            item_list = new ItemList
                            {
                                items = new List<Item>
                                {
                                    new Item
                                    {
                                        name = "Item Name",
                                        currency = "USD",
                                        price = "15",
                                        quantity = "5",
                                        sku = "sku"
                                    }
                                }
                            }
                        }
                    },
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = redirectUrl + "&cancel=true",
                        return_url = redirectUrl
                    }
                };

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.AddNewRequest("Create PayPal payment", payment);
                #endregion

                // Create a payment using a valid APIContext
                var createdPayment = payment.Create(apiContext);

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.RecordResponse(createdPayment);
                #endregion

                // Using the `links` provided by the `createdPayment` object, we can give the user the option to redirect to PayPal to approve the payment.
                var links = createdPayment.links.GetEnumerator();
                while (links.MoveNext())
                {
                    var link = links.Current;
                    if (link.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        this.flow.RecordRedirectUrl("Redirect to PayPal to approve the payment...", link.href);
                    }
                }
                Session.Add(guid, createdPayment.id);
                Session.Add("flow-" + guid, this.flow);
            }
            else
            {
                var guid = Request.Params["guid"];

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow = Session["flow-" + guid] as RequestFlow;
                this.RegisterSampleRequestFlow();
                this.flow.RecordApproval("PayPal payment approved successfully.");
                #endregion

                // Using the information from the redirect, setup the payment to execute.
                var paymentId = Session[guid] as string;
                var paymentExecution = new PaymentExecution() { payer_id = payerId };
                var payment = new Payment() { id = paymentId };

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.AddNewRequest("Execute PayPal payment", payment);
                #endregion

                // Execute the payment.
                var executedPayment = payment.Execute(apiContext, paymentExecution);

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.RecordResponse(executedPayment);
                #endregion

                // A refund transaction. Use the amount to create a refund object
                var refundRequest = new RefundRequest()
                {
                    amount = new Amount()
                    {
                        currency = "USD",
                        total = "100.00"
                    }
                };

                // Get the sale resource from the executed payment's list of related resources.
                var sale = executedPayment.transactions[0].related_resources[0].sale;

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.AddNewRequest("Refund sale", refundRequest, string.Format("URI: /v1/payments/sale/{0}/refund", sale.id));
                #endregion
            
                // Refund by posting Refund object using a valid APIContext
                //change API Context
                apiContext = Configuration.GetAPIContext();
                var response = Sale.Refund(apiContext, sale.id, refundRequest);

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.RecordResponse(response);
                #endregion

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.AddNewRequest("Get the details of the payment", description: "ID: " + executedPayment.id);
                #endregion

                var retrievedPayment = Payment.Get(apiContext, executedPayment.id);

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.RecordResponse(retrievedPayment);
                #endregion

                // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
            }
        }
    }
}
