//==============================================================================
//
//  This file was auto-generated by a tool using the PayPal REST API schema.
//  More information: https://developer.paypal.com/docs/api/
//
//==============================================================================
using Newtonsoft.Json;
using System;

namespace PayPal.Api
{
    /// <summary>
    /// Details about a specific error.
    /// <para>
    /// See <a href="https://developer.paypal.com/docs/api/">PayPal Developer documentation</a> for more information.
    /// </para>
    /// </summary>
    public class ErrorDetails : PayPalSerializableObject
    {
        /// <summary>
        /// Name of the field that caused the error. Required for client side errors.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "field")]
        public string field { get; set; }

        /// <summary>
        /// Reason for the error.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "issue")]
        public string issue { get; set; }

        /// <summary>
        /// Reference ID of the purchase_unit associated with this error
        /// </summary>
        [Obsolete]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "purchase_unit_reference_id")]
        public string purchase_unit_reference_id { get; set; }

        /// <summary>
        /// PayPal internal error code.
        /// </summary>
        [Obsolete]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "code")]
        public string code { get; set; }
    }
}
