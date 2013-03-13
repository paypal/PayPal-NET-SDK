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
        /// The Line 1 of the address (e.g., Number, street, etc). 100 characters max. Required.
        /// </summary>
        /// <value>
        /// The first line of an address, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string line1
		{
			get;
			set;
		}


        /// <summary>
        /// The Line 2 of the address (e.g., Suite, apt #, etc). 100 characters max. Optional
        /// </summary>
        /// <value>
        /// The second line of an address, as string
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string line2
		{
			get;
			set;
		}


        /// <summary>
        /// The city name. 50 characters max. Required.
        /// </summary>
        /// <value>
        /// The city name, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string city
		{
			get;
			set;
		}


        /// <summary>
        /// The 2 letter code for US states, and the equivalent for other countries. 100 characters max. Required.
        /// </summary>
        /// <value>
        /// The 2 letter code for US states, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        /// The Zip code or equivalent is usually required for countries that have them. 20 characters max. Required in certain countries.
        /// </summary>
        /// <value>
        /// The Zip code or equivalent, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string postal_code
		{
			get;
			set;
		}


        /// <summary>
        /// The 2 letter country code. 2 characters max. Required
        /// </summary>
        /// <value>
        /// The 2 letter country code, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string country_code
		{
			get;
			set;
		}


        /// <summary>
        /// Address type; allowable values are residential, business, or mailbox. Optional
        /// </summary>
        /// <value>
        /// The Address type, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string type
		{
			get;
			set;
		}


        /// <summary>
        /// The phone number in e.123 format. Optional.
        /// </summary>
        /// <value>
        /// The phone number, as string.
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
