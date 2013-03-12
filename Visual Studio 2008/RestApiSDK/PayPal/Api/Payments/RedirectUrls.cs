using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Information about the redirection Urls
    /// </summary>
	public class RedirectUrls : Resource  
	{

        /// <summary>
        /// URL to which the buyer’s browser is returned after choosing to pay with PayPal.
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
        /// URL to which the buyer is returned if the buyer does not approve the use of PayPal to pay you.
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
