using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    ///  This object represents a payer’s funding instrument (credit card).
    /// </summary>
	public class FundingInstrument : Resource  
	{

        /// <summary>
        /// Used when the funding instrument is a credit card.  Required if creating a funding instrument.
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
        /// <see cref="CreditCardToken"/> for credit card details stored with PayPal. You can use this in place of a credit card. Required if not passing credit card details.
        /// </summary>
        /// <value>
        /// The credit card token as <see cref="CreditCardToken"/>.
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
