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
    /// A resource representing a credit card that can be used to fund a payment.
    /// </summary>
	public class CreditCard : Resource  
	{

        /// <summary>
        /// The credit card id
        /// </summary>
        /// <value>
        /// The credit card id.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id
		{
			get;
			set;
		}


        /// <summary>
        /// The date the card is valid until.
        /// </summary>
        /// <value>
        /// The date the card is valid until as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string valid_until
		{
			get;
			set;
		}
		

		/// <summary>
		/// state
    	/// </summary>
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
        /// The type or brand of credit card, such as visa, mastercard, or discover.
        /// </summary>
        /// <value>
        /// The type or brand of credit card.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string type
		{
			get;
			set;
		}


        /// <summary>
        /// The credit card number
        /// </summary>
        /// <value>
        /// The credit card number as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string number
		{
			get;
			set;
		}


        /// <summary>
        /// The month the card expires
        /// </summary>
        /// <value>
        /// The month the card expires as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string expire_month
		{
			get;
			set;
		}


        /// <summary>
        /// The year the card expires
        /// </summary>
        /// <value>
        /// The year the card expires as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string expire_year
		{
			get;
			set;
		}


        /// <summary>
        /// The 3-digit Credit Card Code (CCV) on the back of the card.
        /// </summary>
        /// <value>
        /// The 3-digit Credit Card Code (CCV) on the back of the card as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string cvv2
		{
			get;
			set;
		}


        /// <summary>
        /// The first name displayed on the credit card.
        /// </summary>
        /// <value>
        /// The first name displayed on the credit card, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string first_name
		{
			get;
			set;
		}


        /// <summary>
        /// The last name displayed on the credit card.
        /// </summary>
        /// <value>
        /// The last name displayed on the credit card, as string.
        /// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string last_name
		{
			get;
			set;
		}


        /// <summary>
        /// The billing address of the Credit Card
        /// </summary>
        /// <value>
        /// The billing address as <see cref="Address"/>.
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
        /// </value
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
