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
    /// Sale Information
    /// </summary>
	public class Sale : Resource  
	{

        /// <summary>
        /// The ID of the sale transaction.
        /// </summary>
        /// <value>
        /// The ID of the sale transaction as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}


        /// <summary>
        /// The time stamp of when the Sale was created.
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
        /// The time stamp of when the resource was last updated.  Assigned in response
        /// </summary>
        /// <value>
        /// The time stamp of when the resource was last updated as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string update_time
		{
			get;
			set;
		}


        /// <summary>
        /// The current state of the sale. Either pending, completed, refunded, or partially_refunded. Assigned in response
        /// </summary>
        /// <value>
        /// The current state of the Sale as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        ///  Details about the collected amount.
        /// </summary>
        /// <value>
        /// Details about the collected amount as <see cref="Amount"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}


        /// <summary>
        /// The ID of the payment resource on which this <see cref="Transaction"/> is based. Assigned in response
        /// </summary>
        /// <value>
        /// The ID of the payment resource on which this transaction is based as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string parent_payment
		{
			get;
			set;
		}


        /// <summary>
        /// A list of <see cref="Link">Links</see>
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
		/// Get call for Sale.
		/// GET /v1/payments/sale/:saleId
        /// </summary>
        /// <param name="accessToken">Access Token</param>
	 	/// <param name="saleId">SaleId</param>
		/// <returns>Returns Sale object</returns>
		public static Sale Get(string accessToken, string saleId)
		{
			if (String.IsNullOrEmpty(saleId))
			{
				throw new System.ArgumentNullException("saleId cannot be null or empty");
			}
			string pattern = "v1/payments/sale/{0}";
			object[] container = new Object[] { saleId };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = string.Empty;
			return PayPalResource.ConfigureAndExecute<Sale>(accessToken, HttpMethod.GET, resourcePath, payLoad);
		}

		/// <summary>
		/// Refund call for Sale.
		/// POST /v1/payments/sale/:saleId/refund
        /// </summary>
        /// <param name="accessToken">Access Token</param>
	 	/// <param name="refund">Refund</param>
		/// <returns>Returns Refund object</returns>
		public Refund Refund(string accessToken, Refund refund)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Refund(apiContext, refund);
		}
		
		/// <summary>
		/// Refund call for Sale.
		/// POST /v1/payments/sale/:saleId/refund
        /// </summary>
        /// <param name="apiContext">APIContext used for the API call</param>
	 	/// <param name="refund">Refund</param>
		/// <returns>Returns Refund object</returns>
		public Refund Refund(APIContext apiContext, Refund refund)
		{
			if (refund == null)
			{
				throw new System.ArgumentNullException("refund cannot be null");
			}
			if (this.id == null)
			{
				throw new System.ArgumentNullException("Id cannot be null");
			}
			string pattern = "v1/payments/sale/{0}/refund";
			object[] container = new Object[] { this.id };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = refund.ConvertToJson();	
		return PayPalResource.ConfigureAndExecute<Refund>(apiContext, HttpMethod.POST, resourcePath, payLoad);
		}

        /// <summary>
        /// Converts the object to JSON string
        /// </summary>
        /// <returns> the object as a JSON string</returns>
		public new string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
    	
	}
}
