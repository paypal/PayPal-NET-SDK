using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// This object represents a token corresponding to a credit card stored with PayPal.
    /// </summary>
	public class CreditCardToken : Resource  
	{

        /// <summary>
        /// ID of credit card previously stored using /vault/credit-card. Required.
        /// </summary>
        /// <value>
        /// The credit card Id, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string credit_card_id
		{
			get;
			set;
		}


        /// <summary>
        /// The payer id
        /// </summary>
        /// <value>
        /// The payer id, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payer_id
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
