using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// This object defines the payment transaction details.
    /// what is the payment for and who is fulfilling it. Transaction is created with a 'Payee' and 'Amount' types
    /// </summary>
	public class Transaction : Resource  
	{

        /// <summary>
        /// The Amount of funds in this Transaction.
        /// </summary>
        /// <value>
        /// The Amount to capture as <see cref="Amount"/> in this Transaction.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Amount amount
		{
			get;
			set;
		}


        /// <summary>
        /// The payee information, the person receiving the funds.
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
        /// A general description of the transacrion, such as "Artwork Purchase".
        /// </summary>
        /// <value>
        /// The general description of the transacrion as string.
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
