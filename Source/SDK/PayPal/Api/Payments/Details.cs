using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using PayPal;
using PayPal.Util;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{
	public class Details
	{
		/// <summary>
		/// Amount being charged for shipping.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string shipping
		{
			get;
			set;
		}
	
		/// <summary>
		/// Sub-total (amount) of items being paid for.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string subtotal
		{
			get;
			set;
		}
	
		/// <summary>
		/// Amount being charged as tax.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string tax
		{
			get;
			set;
		}
	
		/// <summary>
		/// Fee charged by PayPal. In case of a refund, this is the fee amount refunded to the original receipient of the payment.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string fee
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


