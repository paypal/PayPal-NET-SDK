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
        /// The item name. 127 characters max. Required.
        /// </summary>
        /// <value>
        /// The item name, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string name
		{
			get;
			set;
		}


        /// <summary>
        /// The stock keeping unit corresponding (SKU) to item. 50 characters max. Optional.
        /// </summary>
        /// <value>
        /// The stock keeping unit corresponding (SKU) to item, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string sku
		{
			get;
			set;
		}


        /// <summary>
        /// The item cost. 10 characters max. Required.
        /// </summary>
        /// <value>
        /// The item cost, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string price
		{
			get;
			set;
		}


        /// <summary>
        ///  A 3-character currency code. Currently only USD (US dollars) is supported. Required.
        /// </summary>
        /// <value>
        /// The 3-character currency code, as string. 
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string currency
		{
			get;
			set;
		}


        /// <summary>
        /// Number of a particular item. 10 characters max. Required.
        /// </summary>
        /// <value>
        /// Number of a particular item, as string.
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
