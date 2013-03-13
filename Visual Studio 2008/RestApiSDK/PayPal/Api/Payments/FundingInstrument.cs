using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// A resource representing a Payeer's method of submitting funds.
    /// This is used when funds are submitted from a source otherthan PayPal, such as Credit Card.
    /// </summary>
	public class FundingInstrument : Resource  
	{

        /// <summary>
        /// Used when the funding instrument is a credit card.
        /// </summary>
        /// <value>
        /// The credit card information contained within a <see cref="CreditCard"/> object.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public CreditCard credit_card
		{
			get;
			set;
		}


        /// <summary>
        /// A <see cref="CreditCardToken"/> object
        /// </summary>
        /// <value>
        /// The credit_card_token as <see cref="CreditCardToken"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public CreditCardToken credit_card_token
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
