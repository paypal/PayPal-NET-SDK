using Newtonsoft.Json;

namespace PayPal.Api.OpenIdConnect
{
    /// <summary>
    /// Address used in context of Log In with PayPal.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Street address component, which may include house number, and street name
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "street_address")]
        public string street_address { get; set; }

        /// <summary>
        /// City or locality component
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "locality")]
        public string locality { get; set; }

        /// <summary>
        /// State, province, prefecture or region component
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "region")]
        public string region { get; set; }

        /// <summary>
        /// Zip code or postal code component
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "postal_code")]
        public string postal_code { get; set; }

        /// <summary>
        /// Country name component.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "country")]
        public string country { get; set; }
    }
}
