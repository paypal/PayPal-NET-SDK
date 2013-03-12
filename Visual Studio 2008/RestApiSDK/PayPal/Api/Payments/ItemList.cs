using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

    /// <summary>
    /// a Item List of <see cref="Item"/>s that you are requesting payment for 
    /// </summary>
	public class ItemList : Resource  
	{

        /// <summary>
        /// The list of <see cref="Item"/>s.
        /// </summary>
        /// <value>
        /// The list of <see cref="Item"/>s.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Item> items
		{
			get;
			set;
		}


        /// <summary>
        /// shipping_address
        /// </summary>
        /// <value>
        /// The shipping_address.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ShippingAddress shipping_address
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
