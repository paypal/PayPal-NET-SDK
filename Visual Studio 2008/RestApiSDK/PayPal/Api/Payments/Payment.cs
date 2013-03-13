using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal;
using PayPal.Util;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Payment Information about the <see cref="Transaction"/>
    /// </summary>
	public class Payment : Resource  
	{

        /// <summary>
        /// The ID of the created payment Assigned in response
        /// </summary>
        /// <value>
        /// The Payment id as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}


        /// <summary>
        /// The time stamp of when the payment was created.
        /// Payment creation time as defined in <see href="http://tools.ietf.org/html/rfc3339#section-5.6">RFC 3339 Section 5.6</see>.  Assigned in response 
        /// </summary>
        /// <value>
        /// The timestamp of create time as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string create_time
		{
			get;
			set;
		}


        /// <summary>
        /// The time stamp of when the payment resource was last updated.  Assigned in response
        /// </summary>
        /// <value>
        /// The time stamp of when the payment resource was last updated as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string update_time
		{
			get;
			set;
		}


        /// <summary>
        /// The current payment state, one of the following: created, approved, failed, canceled, or expired. Assigned in response.
        /// </summary>
        /// <value>
        /// The current payment state as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        /// Payment intent; currently only sale is a valid value. 
        /// </summary>
        /// <value>
        /// The method used to processing this payment as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string intent
		{
			get;
			set;
		}


        /// <summary>
        /// The payer information, the person who funds a payment.
        /// </summary>
        /// <value>
        /// The payer information as a <see cref="Payer"/> object.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Payer payer
		{
			get;
			set;
		}


        /// <summary>
        /// Transactional details including the amount and item details.
        /// The Payment creation API requires a list of <see cref="Transaction">Transactions</see>.
        /// </summary>
        /// <value>
        /// The list of <see cref="Transaction"/> objects.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Transaction> transactions
		{
			get;
			set;
		}


        /// <summary>
        /// Contains the URL redirection information, such as where to redirect the user after a successful or canceled transaction.
        /// </summary>
        /// <value>
        /// The <see cref="RedirectUrls"/> object that contains the URL redirection information.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public RedirectUrls redirect_urls
		{
			get;
			set;
		}


        /// <summary>
        /// A list of <see cref="Link"/>s
        /// </summary>
        /// <value>
        /// The list of <see cref="Link"/> objects.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Link> links
		{
			get;
			set;
		}
		

		/// <summary>
		/// Get call for Payment.
        /// <code>
		/// GET /v1/payments/payment?count=:count&start_id=:start_id&start_index=:start_index&start_time=:start_time&end_time=:end_time&payee_id=:payee_id&sort_by=:sort_by&sort_order=:sort_order
        /// </code>
        /// </summary>
        /// <param name="accessToken">Access Token</param>
		/// <param name="parameters">Dictionary holding query string name and values
		/// having the following values for keys
		/// count,
        /// start_id, Resource ID that indicates the starting resource to return.
        /// start_index, Start index of the resources to be returned. Typically used to jump to a specific position in the resource history based on its order.
        /// start_time, Resource creation time that indicates the start of a range of results.
        /// end_time, Resource creation time that indicates the end of a range of results.
		/// payee_id,
        /// sort_by, Sort based on <see cref="create_time"/> or <see cref="update_time"/>.
        /// sort_order, Sort based on order of results. Options include <asc>asc</asc> for ascending order or <dec>dec</dec> for descending order.
		/// All other keys are ignored
		/// </param>
		/// <returns>Returns PaymentHistory object</returns>
		public static PaymentHistory Get(string accessToken, Dictionary<String, String> parameters)
		{
			string pattern = "v1/payments/payment?count={0}&start_id={1}&start_index={2}&start_time={3}&end_time={4}&payee_id={5}&sort_by={6}&sort_order={7}";
			object[] container = new object[] { parameters };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = string.Empty;
			return PayPalResource.ConfigureAndExecute<PaymentHistory>(accessToken, HttpMethod.GET, resourcePath, payLoad);
		}

		/// <summary>
		/// Get call for Payment.
		/// <code>
        /// GET /v1/payments/payment?count=:count&start_id=:start_id&start_index=:start_index&start_time=:start_time&end_time=:end_time&payee_id=:payee_id&sort_by=:sort_by&sort_order=:sort_order
        /// </code>
        /// </summary>
        /// <param name="accessToken">Access Token</param>
		/// <param name="parameters">Container for query strings</param>
		/// <returns>Returns PaymentHistory object</returns>
		public static PaymentHistory Get(string accessToken, QueryParameters parameters)
		{
			string pattern = "v1/payments/payment?count={0}&start_id={1}&start_index={2}&start_time={3}&end_time={4}&payee_id={5}&sort_by={6}&sort_order={7}";
			object[] container = new object[] { parameters };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = string.Empty;
			return PayPalResource.ConfigureAndExecute<PaymentHistory>(accessToken, HttpMethod.GET, resourcePath, payLoad);
		}

		/// <summary>
		/// Create call for Payment.
		/// POST /v1/payments/payment
        /// </summary>
        /// <param name="accessToken">Access Token</param>
		/// <returns>Returns Payment object</returns>
		public Payment Create(string accessToken)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Create(apiContext);
		}
		
		/// <summary>
		/// Create call for Payment.
		/// POST /v1/payments/payment
        /// </summary>
        /// <param name="apiContext">APIContext used for the API call</param>
		/// <returns>Returns Payment object</returns>
		public Payment Create(APIContext apiContext)
		{
			string resourcePath = "v1/payments/payment";
			string payLoad = this.ConvertToJson();	
		return PayPalResource.ConfigureAndExecute<Payment>(apiContext, HttpMethod.POST, resourcePath, payLoad);
		}		

		/// <summary>
		/// Get call for Payment.
		/// GET /v1/payments/payment/:paymentId
        /// </summary>
        /// <param name="accessToken">Access Token</param>
	 	/// <param name="paymentId">PaymentId</param>
		/// <returns>Returns Payment object</returns>
		public static Payment Get(string accessToken, string paymentId)
		{
			if (String.IsNullOrEmpty(paymentId))
			{
				throw new System.ArgumentNullException("paymentId cannot be null or empty");
			}
			string pattern = "v1/payments/payment/{0}";
			object[] container = new Object[] { paymentId };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = string.Empty;
			return PayPalResource.ConfigureAndExecute<Payment>(accessToken, HttpMethod.GET, resourcePath, payLoad);
		}

		/// <summary>
		/// Execute call for Payment.
		/// POST /v1/payments/payment/:paymentId/execute
        /// <param name="accessToken">Access Token</param>
	 	/// <param name="paymentExecution">PaymentExecution</param>
		/// <returns>Returns Payment object</returns>
		/// </summary>
		public Payment Execute(string accessToken, PaymentExecution paymentExecution)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Execute(apiContext, paymentExecution);
		}
		
		/// <summary>
		/// Execute call for Payment.
		/// POST /v1/payments/payment/:paymentId/execute
        /// </summary>
        /// <param name="apiContext">APIContext used for the API call</param>
	 	/// <param name="paymentExecution">PaymentExecution</param>
		/// <returns>Returns Payment object</returns>
		public Payment Execute(APIContext apiContext, PaymentExecution paymentExecution)
		{
			if (paymentExecution == null)
			{
				throw new System.ArgumentNullException("paymentExecution cannot be null");
			}
			if (this.id == null)
			{
				throw new System.ArgumentNullException("Id cannot be null");
			}
			string pattern = "v1/payments/payment/{0}/execute";
			object[] container = new Object[] { this.id };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = paymentExecution.ConvertToJson();	
		return PayPalResource.ConfigureAndExecute<Payment>(apiContext, HttpMethod.POST, resourcePath, payLoad);
		}

        /// <summary>
        /// Converts the object to JSON string
        /// </summary>
        /// <returns>the object as a JSON string</returns>
		public new string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
    	
	}
}
