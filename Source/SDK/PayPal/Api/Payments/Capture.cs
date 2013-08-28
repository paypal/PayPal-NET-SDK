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
	public class Capture
	{
		/// <summary>
		/// Identifier of the Capture transaction.
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
		/// Amount being captured. If no amount is specified, amount is used from the authorization being captured. If amount is same as the amount that's authorized for, the state of the authorization changes to captured. If not, the state of the authorization changes to partially_captured. Alternatively, you could indicate a final capture by seting the is_final_capture flag to true.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}
	
		/// <summary>
		/// whether this is a final capture for the given authorization or not. If it's final, all the remaining funds held by the authorization, will be released in the funding instrument.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool is_final_capture
		{
			get;
			set;
		}
	
		/// <summary>
		/// State of the capture transaction.
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
		/// Obtain the Capture transaction resource for the given identifier.
		/// </summary>
		/// <param name="accessToken">Access Token used for the API call.</param>
		/// <param name="captureId">string</param>
		/// <returns>Capture</returns>
		public static Capture Get(string accessToken, string captureId)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Get(apiContext, captureId);
		}
		
		/// <summary>
		/// Obtain the Capture transaction resource for the given identifier.
		/// </summary>
		/// <param name="apiContext">APIContext used for the API call.</param>
		/// <param name="captureId">string</param>
		/// <returns>Capture</returns>
		public static Capture Get(APIContext apiContext, string captureId)
		{
			if (string.IsNullOrEmpty(apiContext.AccessToken))
			{
				throw new ArgumentNullException("AccessToken cannot be null or empty");
			}
			if (captureId == null)
			{
				throw new ArgumentNullException("captureId cannot be null");
			}
			object[] parameters = new object[] {captureId};
			string pattern = "v1/payments/capture/{0}";
			string resourcePath = SDKUtil.FormatURIPath(pattern, parameters);
			string payLoad = "";
			return PayPalResource.ConfigureAndExecute<Capture>(apiContext, HttpMethod.GET, resourcePath, payLoad);
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
			string pattern = "v1/payments/capture/{0}/refund";
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


