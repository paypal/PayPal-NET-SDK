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
    /// Use this call to refund a completed payment. 
    /// Provide the sale_id in the URI and an empty JSON payload for a full refund. For partial refunds, you can include an amount.
    /// </summary>
	public class Refund : Resource  
	{

        /// <summary>
        ///  The refund id
        /// </summary>
        /// <value>
        /// The refund id as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}


        /// <summary>
        /// The time stamp of when the Refund was created.
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
        /// The current state of the Refund.
        /// </summary>
        /// <value>
        /// The current state of the Refund.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        ///  The Refund details including both refunded amount (to Payer) and refunded fee (to Payee). 
        ///  If amount is not specified, it’s assumed to be full refund. If no amount is provided, you must still provide an empty JSON payload ({}) in the body. Optional
        /// </summary>
        /// <value>
        /// The Refund details including both refunded amount (to Payer) and refunded fee (to Payee) as <see cref="Amount"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}


        /// <summary>
        /// The origional sale ID of the sale transaction being refunded. Assigned in response
        /// </summary>
        /// <value>
        /// The origional sale ID of the sale transaction being refunded as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string sale_id
		{
			get;
			set;
		}


        /// <summary>
        /// capture_id
        /// </summary>
        /// <value>
        /// The capture_id.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string capture_id
		{
			get;
			set;
		}


        /// <summary>
        /// The ID of the payment resource on which this transaction is based. Assigned in response.
        /// </summary>
        /// <value>
        /// The ID of the payment resource on which this transaction is based, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string parent_payment
		{
			get;
			set;
		}


        /// <summary>
        /// The general description for the refund. Optional
        /// </summary>
        /// <value>
        /// The general description for the refund as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string description
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
		/// Get call for Refund.
        /// <code>GET /v1/payments/refund/:refundId</code>
        ///  </summary>       
        /// <param name="accessToken">Access Token</param>
	 	/// <param name="refundId">RefundId</param>
		/// <returns>Returns Refund object</returns>
		public static Refund Get(string accessToken, string refundId)
		{
			if (String.IsNullOrEmpty(refundId))
			{
				throw new System.ArgumentNullException("refundId cannot be null or empty");
			}
			string pattern = "v1/payments/refund/{0}";
			object[] container = new Object[] { refundId };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = string.Empty;
			return PayPalResource.ConfigureAndExecute<Refund>(accessToken, HttpMethod.GET, resourcePath, payLoad);
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
