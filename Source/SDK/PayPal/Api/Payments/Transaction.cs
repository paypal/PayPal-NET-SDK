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
	public class Transaction
	{
		/// <summary>
		/// Amount being collected.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}
	
		/// <summary>
		/// Recepient of the funds in this transaction.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Payee payee
		{
			get;
			set;
		}
	
		/// <summary>
		/// Description of what is being paid for.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string description
		{
			get;
			set;
		}
	
		/// <summary>
		/// List of items being paid for.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ItemList item_list
		{
			get;
			set;
		}
	
		/// <summary>
		/// List of financial transactions (Sale, Authorization, Capture, Refund) related to the payment.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<RelatedResources> related_resources
		{
			get;
			set;
		}
	
		/// <summary>
		/// Additional transactions for complex payment (Parallel and Chained) scenarios.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Transaction> transactions
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


