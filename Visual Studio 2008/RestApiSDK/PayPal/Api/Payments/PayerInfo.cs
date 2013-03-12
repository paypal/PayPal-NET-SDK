using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// The Payer Information object
    /// </summary>
	public class PayerInfo : Resource  
	{

        /// <summary>
        /// The Payer's email address.
        /// </summary>
        /// <value>
        /// The Payer's email address.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string email
		{
			get;
			set;
		}


        /// <summary>
        /// The Payer's first name
        /// </summary>
        /// <value>
        /// The Payer's first name.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string first_name
		{
			get;
			set;
		}


        /// <summary>
        /// The Payer's last name
        /// </summary>
        /// <value>
        /// The Payer's last name.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string last_name
		{
			get;
			set;
		}


        /// <summary>
        /// The Payer's Id
        /// </summary>
        /// <value>
        /// The Payer's Id as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payer_id
		{
			get;
			set;
		}


        /// <summary>
        /// The Payer's shipping address
        /// </summary>
        /// <value>
        /// The Payer's shipping address as <see cref="Address"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Address shipping_address
		{
			get;
			set;
		}


        /// <summary>
        /// The Payer's phone
        /// </summary>
        /// <value>
        /// The Payer's phone.
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
        /// <returns>the object as a JSON string</returns>
		public new string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
    	
	}
}
