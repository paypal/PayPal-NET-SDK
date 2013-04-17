using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayPal.PayPal.Api.Errors
{
	public class Detail
	{
		/// <summary>
		/// field
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string field
		{
			get;
			set;
		}

		/// <summary>
		/// issue
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string issue
		{
			get;
			set;
		}

	}
}
