using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using PayPal;
using PayPal.Util;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{
	public class Payment
	{
		/// <summary>
		/// Identifier of the payment resource created.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}
	
		/// <summary>
		/// Time the resource was created.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string create_time
		{
			get;
			set;
		}
	
		/// <summary>
		/// Time the resource was last updated.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string update_time
		{
			get;
			set;
		}
	
		/// <summary>
		/// Intent of the payment - Sale or Authorization or Order.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string intent
		{
			get;
			set;
		}
	
		/// <summary>
		/// Source of the funds for this payment represented by a PayPal account or a direct credit card.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Payer payer
		{
			get;
			set;
		}
	
		/// <summary>
		/// A payment can have more than one transaction, with each transaction establishing a contract between the payer and a payee
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Transaction> transactions
		{
			get;
			set;
		}
	
		/// <summary>
		/// state of the payment
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}
	
		/// <summary>
		/// Redirect urls required only when using payment_method as PayPal - the only settings supported are return and cancel urls.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public RedirectUrls redirect_urls
		{
			get;
			set;
		}
	
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Links> links
		{
			get;
			set;
		}
	
		/// <summary>
		/// Creates (and processes) a new Payment Resource.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <returns>Payment</returns>
		public Payment Create(string accessToken)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Create(apiContext);
		}
		
		/// <summary>
		/// Creates (and processes) a new Payment Resource.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <returns>Payment</returns>
		public Payment Create(APIContext apiContext)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			string resourcePath = "v1/payments/payment";
			string payLoad = this.ConvertToJson();
			return PayPalResource.ConfigureAndExecute<Payment>(apiContext, HttpMethod.POST, resourcePath, payLoad);
		}
	
		/// <summary>
		/// Obtain the Payment resource for the given identifier.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <param name="paymentId">string</param>
		/// <returns>Payment</returns>
		public static Payment Get(string accessToken, string paymentId)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Get(apiContext, paymentId);
		}
		
		/// <summary>
		/// Obtain the Payment resource for the given identifier.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <param name="paymentId">string</param>
		/// <returns>Payment</returns>
		public static Payment Get(APIContext apiContext, string paymentId)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			if (paymentId == null)
			{
				throw new ArgumentNullException("paymentId cannot be null");
			}
			object[] parameters = new object[] {paymentId};
			string pattern = "v1/payments/payment/{0}";
			string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
			string payLoad = "";
			return PayPalResource.ConfigureAndExecute<Payment>(apiContext, HttpMethod.GET, resourcePath, payLoad);
		}
	
		/// <summary>
		/// Executes the payment (after approved by the Payer) associated with this resource when the payment method is PayPal.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <param name="paymentExecution">PaymentExecution</param>
		/// <returns>Payment</returns>
		public Payment Execute(string accessToken, PaymentExecution paymentExecution)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Execute(apiContext, paymentExecution);
		}
		
		/// <summary>
		/// Executes the payment (after approved by the Payer) associated with this resource when the payment method is PayPal.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <param name="paymentExecution">PaymentExecution</param>
		/// <returns>Payment</returns>
		public Payment Execute(APIContext apiContext, PaymentExecution paymentExecution)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			if (this.id == null)
			{
				throw new ArgumentNullException("Id cannot be null");
			}
			if (paymentExecution == null)
			{
				throw new ArgumentNullException("paymentExecution cannot be null");
			}
			object[] parameters = new object[] {this.id};
			string pattern = "v1/payments/payment/{0}/execute";
			string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
			string payLoad = paymentExecution.ConvertToJson();
			return PayPalResource.ConfigureAndExecute<Payment>(apiContext, HttpMethod.POST, resourcePath, payLoad);
		}
	
		/// <summary>
		/// Retrieves a list of Payment resources.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <param name="containerDictionary">Dictionary<String, String></param>
		/// <returns>PaymentHistory</returns>
		public static PaymentHistory List(string accessToken, Dictionary<String, String> containerDictionary)
		{
			APIContext apiContext = new APIContext(accessToken);
			return List(apiContext, containerDictionary);
		}
		
		/// <summary>
		/// Retrieves a list of Payment resources.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <param name="containerDictionary">Dictionary<String, String></param>
		/// <returns>PaymentHistory</returns>
		public static PaymentHistory List(APIContext apiContext, Dictionary<String, String> containerDictionary)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			if (containerDictionary == null)
			{
				throw new ArgumentNullException("containerDictionary cannot be null");
			}
			object[] parameters = new object[] {containerDictionary};
			string pattern = "v1/payments/payment?count={0}&start_id={1}&start_index={2}&start_time={3}&end_time={4}&payee_id={5}&sort_by={6}&sort_order={7}";
			string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
			string payLoad = "";
			return PayPalResource.ConfigureAndExecute<PaymentHistory>(apiContext, HttpMethod.GET, resourcePath, payLoad);
		}

        /// <summary>
        /// Retrieves a list of Payment resources.
        /// </summary>
        [Obsolete("Use List method")]
        public static PaymentHistory Get(APIContext apiContext, QueryParameters queryParameters)
        {
            if (string.IsNullOrEmpty(apiContext.AccessToken))
            {
                throw new ArgumentNullException("AccessToken cannot be null or empty");
            }
            if (queryParameters == null)
            {
                throw new ArgumentNullException("queryParameters cannot be null");
            }
            object[] parameters = new object[] { queryParameters };
            string pattern = "v1/payments/payment?count={0}&start_id={1}&start_index={2}&start_time={3}&end_time={4}&payee_id={5}&sort_by={6}&sort_order={7}";
            string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
            string payLoad = "";
            return PayPalResource.ConfigureAndExecute<PaymentHistory>(apiContext, HttpMethod.GET, resourcePath, payLoad);
        }

        /// <summary>
        /// Retrieves a list of Payment resources.
        /// </summary>
        [Obsolete("Use List method")]
        public static PaymentHistory Get(string accessToken, QueryParameters queryParameters)
        {
            APIContext apiContext = new APIContext(accessToken);
            return Get(apiContext, queryParameters);
        }
	
		/// <summary>
		/// Converts the object to JSON string
		/// </summary>
		public string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
	}
}


