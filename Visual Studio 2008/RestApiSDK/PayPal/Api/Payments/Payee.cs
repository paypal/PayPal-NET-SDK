using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// A resource representing a Payee that receives funds from a <see cref="Payment"/>.
    /// </summary>
	public class Payee : Resource  
	{

        /// <summary>
        ///  The merchant id
        /// </summary>
        /// <value>
        /// The merchant id.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string merchant_id
		{
			get;
			set;
		}


        /// <summary>
        /// Payee email
        /// </summary>
        /// <value>
        /// The Payee email.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string email
		{
			get;
			set;
		}


        /// <summary>
        /// Payee phone
        /// </summary>
        /// <value>
        /// The Payee phone.
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
