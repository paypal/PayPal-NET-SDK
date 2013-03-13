using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Payment History
    /// </summary>
	public class PaymentHistory : Resource  
	{

        /// <summary>
        /// The list of payments.
        /// </summary>
        /// <value>
        /// The payments as a list of <see cref="Payment"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Payment> payments
		{
			get;
			set;
		}


        /// <summary>
        /// The count of payments.
        /// </summary>
        /// <value>
        /// The count of payments.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int? count
		{
			get;
			set;
		}


        /// <summary>
        /// next id
        /// </summary>
        /// <value>
        /// The next id.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string next_id
		{
			get;
			set;
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
