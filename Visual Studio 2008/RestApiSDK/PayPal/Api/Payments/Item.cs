using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

    /// <summary>
    /// a invoice item to be added to the <see cref="ItemList" />.
    /// </summary>
	public class Item : Resource  
	{

        /// <summary>
        /// The name of the item.
        /// </summary>
        /// <value>
        /// The name of the item as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string name
		{
			get;
			set;
		}


        /// <summary>
        /// The item's sku code.
        /// </summary>
        /// <value>
        /// The item's sku code as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string sku
		{
			get;
			set;
		}


        /// <summary>
        /// The selling price of the item.
        /// </summary>
        /// <value>
        /// The selling price of the item as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string price
		{
			get;
			set;
		}


        /// <summary>
        ///  A 3-character currency code. (default is USD)
        /// </summary>
        /// <value>
        /// The 3-character currency code as string. 
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string currency
		{
			get;
			set;
		}


        /// <summary>
        /// The Item quantity
        /// </summary>
        /// <value>
        /// The Item quantity as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string quantity
		{
			get;
			set;
		}


        /// <summary>
        /// Converts the object to JSON string
        /// </summary>
        /// <returns> the object as a JSON string</returns>>
		public new string ConvertToJson() 
    	{ 
    		return JsonFormatter.ConvertToJson(this);
    	}
    	
	}
}
