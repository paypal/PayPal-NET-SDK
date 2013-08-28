using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using PayPal;
using PayPal.Util;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{
	public class Payer
	{
		/// <summary>
		/// Payment method being used - PayPal Wallet payment or Direct Credit card.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payment_method
		{
			get;
			set;
		}
	
		/// <summary>
		/// List of funding instruments from where the funds of the current payment come from. Typically a credit card.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<FundingInstrument> funding_instruments
		{
			get;
			set;
		}
	
		/// <summary>
		/// Information related to the Payer. In case of PayPal Wallet payment, this information will be filled in by PayPal after the user approves the payment using their PayPal Wallet. 
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public PayerInfo payer_info
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


