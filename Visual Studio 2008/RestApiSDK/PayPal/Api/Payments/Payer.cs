using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

    /// <summary>
    /// A resource representing a Payer that funds a payment 
    /// </summary>
	public class Payer : Resource  
	{

        /// <summary>
        /// The payment method to be used by the payer. (Typically set to "paypal")
        /// </summary>
        /// <value>
        /// The payment method to be used by the payer as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payment_method
		{
			get;
			set;
		}


        /// <summary>
        /// The payer information
        /// </summary>
        /// <value>
        /// The payer info as a <see cref="PayerInfo"/> object.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public PayerInfo payer_info
		{
			get;
			set;
		}


        /// <summary>
        /// The List of funding instruments
        /// </summary>
        /// <value>
        /// The List of funding instruments.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<FundingInstrument> funding_instruments
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
