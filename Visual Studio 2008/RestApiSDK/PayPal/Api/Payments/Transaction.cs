using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// Transaction Information
    /// </summary>
	public class Transaction : Resource  
	{

        /// <summary>
        /// The Amount to capture.
        /// </summary>
        /// <value>
        /// The Amount to capture as <see cref="Amount"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}


        /// <summary>
        /// The payee information
        /// </summary>
        /// <value>
        /// The payee information as <see cref="Payee"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Payee payee
		{
			get;
			set;
		}


        /// <summary>
        /// The description of the transacrion.
        /// </summary>
        /// <value>
        /// The description of the transacrion as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string description
		{
			get;
			set;
		}


        /// <summary>
        /// The <see cref="ItemList"/>
        /// </summary>
        /// <value>
        ///  The <see cref="ItemList"/> of <see cref="Item"/>s.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ItemList item_list
		{
			get;
			set;
		}


        /// <summary>
        /// The related resources as a list of <see cref="SubTransaction"/>.
        /// </summary>
        /// <value>
        /// The related resources as a list of <see cref="SubTransaction"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<SubTransaction> related_resources
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
