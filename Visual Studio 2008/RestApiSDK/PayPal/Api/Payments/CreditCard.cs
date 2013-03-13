using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayPal;
using PayPal.Util;
using PayPal.Api.Payments;

namespace PayPal.Api.Payments
{

	/// <summary>
    /// A resource  represents a payer’s funding instrument, such as a credit card or token that represents a credit card.
    /// </summary>
	public class CreditCard : Resource  
	{

        /// <summary>
        /// The ID of the credit card. This ID is provided in the response when storing credit cards. Required if using a stored credit card.
        /// </summary>
        /// <value>
        /// The ID of the credit card, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}


        /// <summary>
        /// Funding instrument expiration date. Assigned in response
        /// </summary>
        /// <value>
        /// The funding instrument expiration date, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string valid_until
		{
			get;
			set;
		}


        /// <summary>
        /// State of the credit card funding instrument: expired or ok. Assigned in response
        /// </summary>
        /// <value>
        /// The state of the credit card funding instrument, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string state
		{
			get;
			set;
		}


        /// <summary>
        /// The payer id
        /// </summary>
        /// <value>
        /// The payer id as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string payer_id
		{
			get;
			set;
		}


        /// <summary>
        /// The Credit card type. Valid types are: Visa, MasterCard, Discover, Amex Required.
        /// </summary>
        /// <value>
        /// The Credit card type, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string type
		{
			get;
			set;
		}


        /// <summary>
        /// The Credit card number. Numeric characters only with no spaces or punctuation. The string must conform with modulo and length required by each credit card type. Redacted in responses. Required.
        /// </summary>
        /// <value>
        /// The Credit card number, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string number
		{
			get;
			set;
		}


        /// <summary>
        /// The 1-2 digit expiration month. Required.
        /// </summary>
        /// <value>
        /// The 1-2 digit expiration month, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string expire_month
		{
			get;
			set;
		}


        /// <summary>
        /// The 4-digit expiration year. Required.
        /// </summary>
        /// <value>
        /// The 4-digit expiration year, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string expire_year
		{
			get;
			set;
		}


        /// <summary>
        /// The 3-4 digit Credit Card Code (CCV) on the back of the card.
        /// </summary>
        /// <value>
        /// The 3-4 digit Credit Card Code, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string cvv2
		{
			get;
			set;
		}


        /// <summary>
        /// The cardholder’s first name. Optional
        /// </summary>
        /// <value>
        /// The cardholder’s first name, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string first_name
		{
			get;
			set;
		}


        /// <summary>
        /// The cardholder’s last name. Optional
        /// </summary>
        /// <value>
        /// The cardholder’s last name, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string last_name
		{
			get;
			set;
		}


        /// <summary>
        /// The billing address associated with card. Optional
        /// </summary>
        /// <value>
        /// The billing address associated with card, as <see cref="Address"/>.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Address billing_address
		{
			get;
			set;
		}


        /// <summary>
        /// A list of <see cref="Link">Links</see>.
        /// </summary>
        /// <value>
        /// The list of <see cref="Link"/> objects.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Link> links
		{
			get;
			set;
		}
		

		/// <summary>
		/// Create call for CreditCard.
		/// POST /v1/vault/credit-card
        /// </summary>
        /// <param name="accessToken">Access Token</param>
		/// <returns>Returns CreditCard object</returns>
		public CreditCard Create(string accessToken)
		{
			APIContext apiContext = new APIContext(accessToken);
			return Create(apiContext);
		}
		
		/// <summary>
		/// Create call for CreditCard.
		/// POST /v1/vault/credit-card
        /// </summary>
        /// <param name="apiContext">APIContext used for the API call</param>
		/// <returns>Returns CreditCard object</returns>
		public CreditCard Create(APIContext apiContext)
		{
			string resourcePath = "v1/vault/credit-card";
			string payLoad = this.ConvertToJson();	
		return PayPalResource.ConfigureAndExecute<CreditCard>(apiContext, HttpMethod.POST, resourcePath, payLoad);
		}		

		/// <summary>
		/// Get call for CreditCard.
		/// GET /v1/vault/credit-card/:creditCardId
        /// </summary>
        /// <param name="accessToken">Access Token</param>
	 	/// <param name="creditCardId">CreditCardId</param>
		/// <returns>Returns CreditCard object</returns>
		public static CreditCard Get(string accessToken, string creditCardId)
		{
			if (String.IsNullOrEmpty(creditCardId))
			{
				throw new System.ArgumentNullException("creditCardId cannot be null or empty");
			}
			string pattern = "v1/vault/credit-card/{0}";
			object[] container = new Object[] { creditCardId };
			string resourcePath = SDKUtil.FormatURIPath(pattern, container);
			string payLoad = string.Empty;
			return PayPalResource.ConfigureAndExecute<CreditCard>(accessToken, HttpMethod.GET, resourcePath, payLoad);
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
