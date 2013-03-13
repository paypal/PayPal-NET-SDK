using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// The Base Address object used as shipping or billing address in a payment.
    /// </summary>
	public class Address : Resource  
	{

        /// <summary>
        /// The first line of an address, typically the street information
        /// </summary>
        /// <value>
        /// The first line of an address as string
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string line1
		{
			get;
			set;
		}


        /// <summary>
        /// The second line of an address, typically the Apt or Suite, etc information
        /// </summary>
        /// <value>
        /// The second line of an address as string
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string line2
		{
			get;
			set;
		}


        /// <summary>
        /// The city name
        /// </summary>
        /// <value>
        /// The city name as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string city
		{
			get;
			set;
		}


        /// <summary>
        /// The State or province name
        /// </summary>
        /// <value>
        /// The State or province name as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        /// The U.S. ZIP code or other country-specific postal code. 
        /// </summary>
        /// <value>
        /// The U.S. ZIP code or other country-specific postal code as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string postal_code
		{
			get;
			set;
		}


        /// <summary>
        /// The Country code
        /// </summary>
        /// <value>
        /// The Country code.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string country_code
		{
			get;
			set;
		}


        /// <summary>
        /// type
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string type
		{
			get;
			set;
		}


        /// <summary>
        /// The Phone number.
        /// </summary>
        /// <value>
        /// The Phone number.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string phone
		{
			get;
			set;
		}


        /// <summary>
        /// Converts the object to JSON string
        /// </summary>
        /// <returns> the object as a JSON string</returns>
		public new string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
    	
	}
}
