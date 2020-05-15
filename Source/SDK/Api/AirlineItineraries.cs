using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PayPal.Api
{
    /// <summary>
    /// Airline Itineraries Object used to fill travel_itineraries object in Payment Object
    /// </summary>
    public class AirlineItineraries
    {
        /// <summary>
        /// Name of the passgenger
        /// </summary>

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "passenger_name")]
        public string passenger_name { get; set; }
        /// <summary>
        /// Issued date of the ticket
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "issued_date")]
        public string issued_date { get; set; }
        /// <summary>
        /// Travel ageny name
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "travel_agency_name")]
        public string travel_agency_name { get; set; }
        /// <summary>
        /// Travel agency code
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "travel_agency_code")]
        public string travel_agency_code { get; set; }
        /// <summary>
        /// Ticket Number
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ticket_number")]
        public string ticket_number { get; set; }
        /// <summary>
        /// Carrier code of the ticket issuer
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "issuing_carrier_code")]
        public string issuing_carrier_code { get; set; }
        /// <summary>
        /// Customer Code
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "customer_code")]
        public string customer_code { get; set; }
        /// <summary>
        /// Total fare
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total_fare")]
        public Currency total_fare { get; set; }
        /// <summary>
        /// Total Tax
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total_tax")]
        public Currency total_tax { get; set; }
        /// <summary>
        /// Total Fee
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total_fee")]
        public Currency total_fee { get; set; }

        /// <summary>
        /// Sequence of boarding or clearing
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "clearing_sequence")]
        public int clearing_sequence { get; set; }

        /// <summary>
        /// Clearing count
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "clearing_count")]
        public int clearing_count { get; set; }

        /// <summary>
        /// Restricted ticket
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "restricted_ticket")]
        public bool restricted_ticket { get; set; }

        /// <summary>
        /// Airline leg details
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "flight_details")]
        public List<FlightDetails> flight_details { get; set; }

        /// <summary>
        /// Set to true if yoy want UATP details back
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "return_uatp_details")]
        public bool return_uatp_details { get; set; }
        /// <summary>
        /// If this is an UATP transaction
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "uatp_transaction")]
        public bool uatp_transaction { get; set; }

    }
}
