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
	public class Refund
	{
		/// <summary>
		/// Identifier of the refund transaction.
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
		/// Details including both refunded amount (to Payer) and refunded fee (to Payee).If amount is not specified, it's assumed to be full refund.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}
	
		/// <summary>
		/// State of the refund transaction.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}
	
		/// <summary>
		/// ID of the Sale transaction being refunded. 
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string sale_id
		{
			get;
			set;
		}
	
		/// <summary>
		/// ID of the Capture transaction being refunded. 
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string capture_id
		{
			get;
			set;
		}
	
		/// <summary>
		/// ID of the Payment resource that this transaction is based on.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string parent_payment
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
		/// Obtain the Refund transaction resource for the given identifier.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <param name="refundId">string</param>
		/// <returns>Refund</returns>
		public static Refund Get(string accessToken, string refundId)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Get(apiContext, refundId);
		}
		
		/// <summary>
		/// Obtain the Refund transaction resource for the given identifier.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <param name="refundId">string</param>
		/// <returns>Refund</returns>
		public static Refund Get(APIContext apiContext, string refundId)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			if (refundId == null)
			{
				throw new ArgumentNullException("refundId cannot be null");
			}
			object[] parameters = new object[] {refundId};
			string pattern = "v1/payments/refund/{0}";
			string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
			string payLoad = "";
			return PayPalResource.ConfigureAndExecute<Refund>(apiContext, HttpMethod.GET, resourcePath, payLoad);
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


