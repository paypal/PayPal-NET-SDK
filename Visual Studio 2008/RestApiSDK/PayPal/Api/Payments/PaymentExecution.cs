using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Payment Execution
    /// </summary>
	public class PaymentExecution : Resource  
	{

        /// <summary>
        /// the payer id
        /// </summary>
        /// <value>
        /// The payer id.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payer_id
		{
			get;
			set;
		}


        /// <summary>
        /// The list of transactions smounts
        /// </summary>
        /// <value>
        /// The transactions as a List of <see cref="Amount"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Amount> transactions
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
