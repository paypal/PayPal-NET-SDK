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
    /// Payment Information
    /// </summary>
	public class Payment : Resource  
	{

        /// <summary>
        /// The Payment id
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
        /// The time stamp of when the payment was created
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
        /// The time stamp of when the payment is updated.
        /// </summary>
        /// <value>
        /// The time stamp of when the payment is updated as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string update_time
		{
			get;
			set;
		}


        /// <summary>
        /// The current payment state.
        /// </summary>
        /// <value>
        /// The current payment state.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        /// intent
        /// </summary>
        /// <value>
        /// The intent.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string intent
		{
			get;
			set;
		}


        /// <summary>
        /// The payer information
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
        /// The list of transactions.
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
		/// GET /v1/payments/payment?count=:count&start_id=:start_id&start_index=:start_index&start_time=:start_time&end_time=:end_time&payee_id=:payee_id&sort_by=:sort_by&sort_order=:sort_order
        /// </summary>
        /// <param name="accessToken">Access Token</param>
		/// <param name="parameters">Dictionary holding query string name and values
		/// having the following values for keys
		/// count,
		/// start_id,
		/// start_index,
		/// start_time,
		/// end_time,
		/// payee_id,
		/// sort_by,
		/// sort_order,
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
		/// GET /v1/payments/payment?count=:count&start_id=:start_id&start_index=:start_index&start_time=:start_time&end_time=:end_time&payee_id=:payee_id&sort_by=:sort_by&sort_order=:sort_order
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
