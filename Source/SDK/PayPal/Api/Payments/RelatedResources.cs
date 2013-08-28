using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using PayPal;
using PayPal.Util;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{
	public class RelatedResources
	{
		/// <summary>
		/// A sale transaction
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Sale sale
		{
			get;
			set;
		}
	
		/// <summary>
		/// An authorization transaction
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Authorization authorization
		{
			get;
			set;
		}
	
		/// <summary>
		/// A capture transaction
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Capture capture
		{
			get;
			set;
		}
	
		/// <summary>
		/// A refund transaction
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Refund refund
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


