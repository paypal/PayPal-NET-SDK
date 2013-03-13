using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

    /// <summary>
    /// Let's you specify details of a payment amount, such as tax, shipping, and the total.
    /// </summary>
	public class AmountDetails : Resource  
	{

        /// <summary>
        /// The subtotal of all the <see cref="Item">Items</see>
        /// </summary>
        /// <value>
        /// The subtotal of all the <see cref="Item">Items</see> as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string subtotal
		{
			get;
			set;
		}


        /// <summary>
        /// The total tax fee being applied, this is not for the tax rate.
        /// </summary>
        /// <value>
        ///  The total tax fee being applied as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string tax
		{
			get;
			set;
		}


        /// <summary>
        /// The total tax fee being applied.
        /// </summary>
        /// <value>
        /// The total tax fee being applied as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string shipping
		{
			get;
			set;
		}


        /// <summary>
        /// Any additional fees being applied.
        /// </summary>
        /// <value>
        /// The additional fees being applied as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string fee
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
