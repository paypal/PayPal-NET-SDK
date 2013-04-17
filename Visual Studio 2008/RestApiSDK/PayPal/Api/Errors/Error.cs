using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PayPal.PayPal.Api.Errors
{
	public class Error
	{
		/// <summary>
		/// name
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string name
		{
			get;
			set;
		}

		/// <summary>
		/// message
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string message
		{
			get;
			set;
		}

		/// <summary>
		/// information_link
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string information_link
		{
			get;
			set;
		}

		/// <summary>
		/// details
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Detail> details
		{
			get;
			set;
		}
	}
}
