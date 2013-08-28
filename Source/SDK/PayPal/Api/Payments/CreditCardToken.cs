using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using PayPal;
using PayPal.Util;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{
	public class CreditCardToken
	{
		/// <summary>
		/// ID of a previously saved Credit Card resource using /vault/credit-card API.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string credit_card_id
		{
			get;
			set;
		}
	
		/// <summary>
		/// The unique identifier of the payer used when saving this credit card using /vault/credit-card API.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payer_id
		{
			get;
			set;
		}
	
		/// <summary>
		/// Last 4 digits of the card number from the saved card.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string last4
		{
			get;
			set;
		}
	
		/// <summary>
		/// Type of the Card (eg. visa, mastercard, etc.) from the saved card. Please note that the values are always in lowercase and not meant to be used directly for display.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string type
		{
			get;
			set;
		}
	
		/// <summary>
		/// card expiry month from the saved card with value 1 - 12
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int expire_month
		{
			get;
			set;
		}
	
		/// <summary>
		/// 4 digit card expiry year from the saved card
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int expire_year
		{
			get;
			set;
		}
	
		/// <summary>
		/// Converts the object to JSON string
		/// </summary>
		public string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
	}
}


