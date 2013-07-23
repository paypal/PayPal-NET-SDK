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
	public class Sale
	{
		/// <summary>
		/// Identifier of the authorization transaction.
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
		/// Amount being collected.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}
	
		/// <summary>
		/// State of the sale transaction.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
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
		/// Obtain the Sale transaction resource for the given identifier.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <param name="saleId">string</param>
		/// <returns>Sale</returns>
		public static Sale Get(string accessToken, string saleId)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Get(apiContext, saleId);
		}
		
		/// <summary>
		/// Obtain the Sale transaction resource for the given identifier.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <param name="saleId">string</param>
		/// <returns>Sale</returns>
		public static Sale Get(APIContext apiContext, string saleId)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			if (saleId == null)
			{
				throw new ArgumentNullException("saleId cannot be null");
			}
			object[] parameters = new object[] {saleId};
			string pattern = "v1/payments/sale/{0}";
			string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
			string payLoad = "";
			return PayPalResource.ConfigureAndExecute<Sale>(apiContext, HttpMethod.GET, resourcePath, payLoad);
		}
	
		/// <summary>
		/// Creates (and processes) a new Refund Transaction added as a related resource.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <param name="refund">Refund</param>
		/// <returns>Refund</returns>
		public Refund Refund(string accessToken, Refund refund)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Refund(apiContext, refund);
		}
		
		/// <summary>
		/// Creates (and processes) a new Refund Transaction added as a related resource.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <param name="refund">Refund</param>
		/// <returns>Refund</returns>
		public Refund Refund(APIContext apiContext, Refund refund)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			if (this.id == null)
			{
				throw new ArgumentNullException("Id cannot be null");
			}
			if (refund == null)
			{
				throw new ArgumentNullException("refund cannot be null");
			}
			object[] parameters = new object[] {this.id};
			string pattern = "v1/payments/sale/{0}/refund";
			string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
			string payLoad = refund.ConvertToJson();
			return PayPalResource.ConfigureAndExecute<Refund>(apiContext, HttpMethod.POST, resourcePath, payLoad);
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


