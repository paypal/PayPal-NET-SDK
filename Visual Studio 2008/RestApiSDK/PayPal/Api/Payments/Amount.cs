using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Used to specify a payment amount.
    /// </summary>
	public class Amount : Resource  
	{

        /// <summary>
        /// The total must be equal to sum of shipping, tax and subtotal.
        /// </summary>
        /// <value>
        /// The total sum being charged as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string total
		{
			get;
			set;
		}


        /// <summary>
        /// A 3-character currency code. (The US currency code is USD)
        /// </summary>
        /// <value>
        /// The 3-character currency code.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string currency
		{
			get;
			set;
		}


        /// <summary>
        /// The details of a payment amount (Tax, Shipping, Total) contained in a <see cref="AmountDetails"/> object.
        /// </summary>
        /// <value>
        /// The details of a payment amount contained in a <see cref="AmountDetails"/> object.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public AmountDetails details
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
