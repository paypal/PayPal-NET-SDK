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
    /// Refund Information Object
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
        /// The time stamp of when the Refund was created
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
        /// The time stamp of when the Refund is updated.
        /// </summary>
        /// <value>
        /// The time stamp of when the Refund is updated as string.
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
        ///  The refund amount.
        /// </summary>
        /// <value>
        /// The refund amount.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}


        /// <summary>
        /// The origional sale id, that you are refunding
        /// </summary>
        /// <value>
        /// The origional sale id, that you are refunding as string.
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
        /// parent_payment
        /// </summary>
        /// <value>
        /// The parent_payment.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string parent_payment
		{
			get;
			set;
		}


        /// <summary>
        /// description
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string description
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
		/// Get call for Refund.
		/// GET /v1/payments/refund/:refundId
        /// <param name="accessToken">Access Token</param>
	 	/// <param name="refundId">RefundId</param>
		/// <returns>Returns Refund object</returns>
		/// </summary>
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
