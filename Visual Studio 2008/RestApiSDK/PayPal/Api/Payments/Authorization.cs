using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Authorization object
    /// </summary>
	public class Authorization : Resource  
	{

        /// <summary>
        /// The ID of the Authorization transaction.
        /// </summary>
        /// <value>
        /// The ID of the Authorization transaction as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}


        /// <summary>
        /// The time stamp of when the Authorization was created.
        /// Payment creation time as defined in <see href="http://tools.ietf.org/html/rfc3339#section-5.6">RFC 3339 Section 5.6</see>.  Assigned in response 
        /// </summary>
        /// <value>
        /// The timestamp of create time as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string create_time
		{
			get;
			set;
		}


        /// <summary>
        /// The time stamp of when the resource was last updated.  Assigned in response
        /// </summary>
        /// <value>
        /// The time stamp of when the resource was last updated as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string update_time
		{
			get;
			set;
		}


        /// <summary>
        /// The current state of Authorization.
        /// </summary>
        /// <value>
        /// The current state of Authorization, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        ///  Details about the amount.
        /// </summary>
        /// <value>
        /// Details about the amount as <see cref="Amount"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}


        /// <summary>
        /// The ID of the payment resource on which this <see cref="Transaction"/> is based. Assigned in response
        /// </summary>
        /// <value>
        /// The ID of the payment resource on which this transaction is based as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string parent_payment
		{
			get;
			set;
		}


        /// <summary>
        /// A list of <see cref="Link">Links</see>
        /// </summary>
        /// <value>
        /// The list of <see cref="Link"/> objects.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Link> links
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
