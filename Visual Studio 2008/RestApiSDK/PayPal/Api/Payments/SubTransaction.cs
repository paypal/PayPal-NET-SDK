using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// SubTransaction
    /// </summary>
	public class SubTransaction : Resource  
	{

		/// <summary>
		/// sale
    	/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Sale sale
		{
			get;
			set;
		}


        /// <summary>
        /// authorization
        /// </summary>
        /// <value>
        /// The authorization.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Authorization authorization
		{
			get;
			set;
		}


        /// <summary>
        /// refund
        /// </summary>
        /// <value>
        /// The refund.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Refund refund
		{
			get;
			set;
		}


        /// <summary>
        /// capture
        /// </summary>
        /// <value>
        /// The capture.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Capture capture
		{
			get;
			set;
		}


        /// <summary>
        /// Converts the object to JSON string
        /// </summary>
        /// <returns> the object as a JSON string</returns
		public new string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
    	
	}
}
