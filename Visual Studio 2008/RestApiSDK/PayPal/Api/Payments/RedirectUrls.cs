using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Information about the redirection Urls you provide only for PayPal-based payments.
    /// </summary>
	public class RedirectUrls : Resource  
	{

        /// <summary>
        /// The payer is redirected to this URL after approving the payment. Required for PayPal account payments.
        /// </summary>
        /// <value>
        /// The return url as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string return_url
		{
			get;
			set;
		}


        /// <summary>
        /// The payer is redirected to this URL after canceling the payment. Required for PayPal account payments.
        /// </summary>
        /// <value>
        /// The cancel url as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string cancel_url
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
