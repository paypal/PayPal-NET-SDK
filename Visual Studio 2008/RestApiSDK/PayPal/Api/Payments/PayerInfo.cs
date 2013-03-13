using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// The Payer Information object.
    /// This object is pre-filled by PayPal when the payment_method is paypal.
    /// </summary>
	public class PayerInfo : Resource  
	{

        /// <summary>
        /// The Email address representing the payer. 127 characters max. Optional
        /// </summary>
        /// <value>
        /// The Email address representing the payer, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string email
		{
			get;
			set;
		}


        /// <summary>
        /// The first name of the payer. Assigned in response.
        /// </summary>
        /// <value>
        /// The first name of the payer, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string first_name
		{
			get;
			set;
		}


        /// <summary>
        /// The last name of the payer. Assigned in response.
        /// </summary>
        /// <value>
        /// The last name of the payer, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string last_name
		{
			get;
			set;
		}


        /// <summary>
        /// The PayPal assigned Payer ID. Optional.
        /// </summary>
        /// <value>
        /// The PayPal assigned Payer ID, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payer_id
		{
			get;
			set;
		}


        /// <summary>
        /// The shipping address of payer PayPal account.
        /// </summary>
        /// <value>
        /// The shipping address of payer PayPal account, as <see cref="Address"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Address shipping_address
		{
			get;
			set;
		}


        /// <summary>
        /// The phone number representing the payer. 20 characters max. Optional.
        /// </summary>
        /// <value>
        /// The phone number representing the payer, as string.
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
