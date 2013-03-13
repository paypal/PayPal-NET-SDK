using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
	/// 
    /// </summary>
	public class Capture : Resource  
	{

		/// <summary>
		/// id
    	/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}


        /// <summary>
        /// The time stamp of when the Capture was created.
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
		/// state
    	/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}
		

		/// <summary>
		/// amount
    	/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}
		

		/// <summary>
		/// parent_payment
    	/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string parent_payment
		{
			get;
			set;
		}
		

		/// <summary>
		/// authorization_id
    	/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string authorization_id
		{
			get;
			set;
		}
		

		/// <summary>
		/// description
    	/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string description
		{
			get;
			set;
		}
		

		/// <summary>
		/// links
    	/// </summary>
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
