using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

    /// <summary>
    /// a Item List of <see cref="Item">Items</see> that you are identifying in the payment request or refund.
    /// </summary>
	public class ItemList : Resource  
	{

        /// <summary>
        /// The list of <see cref="Item">Items</see>.
        /// </summary>
        /// <value>
        /// The list of <see cref="Item">Items</see>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Item> items
		{
			get;
			set;
		}


        /// <summary>
        /// The address of where the items will be shipped from.
        /// </summary>
        /// <value>
        /// The address of where the items will be shipped from.
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
