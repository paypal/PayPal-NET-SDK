using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Api
{
    /// <summary>
    /// Flight_details for filling flight_details array in Travel_Itineraries
    /// </summary>
    public class FlightDetails
    {
        /// <summary>
        /// Conjuction Ticket
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "conjunction_ticket")]
        public string conjunction_ticket { get; set; }
        /// <summary>
        /// Exchange ticket
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "exchange_ticket")]
        public string exchange_ticket { get; set; }
        /// <summary>
        /// Coupon number used while purchasing the ticket
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "coupon_number")]
        public string coupon_number { get; set; }
        
        /// <summary>
        /// Service class for which the airline ticket is available
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "service_class")]
        public string service_class { get; set; }
        /// <summary>
        /// Travel date for the current leg of travel
        /// Format YYYY-MM-DD
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "travel_date")]
        public string travel_date { get; set; }

        /// <summary>
        /// Carrier code for current leg of travel
        /// </summary>

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "carrier_code")]
        public string carrier_code { get; set; }
        /// <summary>
        /// Stop over permitted during current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "stopover_permitted")]
        public bool stopover_permitted { get; set; }
        /// <summary>
        /// Departure airport for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "departure_airport")]
        public string departure_airport { get; set; }
        /// <summary>
        /// Arrival airport for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "arrival_airport")]
        public string arrival_airport { get; set; }
        /// <summary>
        /// Flight number for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "flight_number")]
        public string flight_number { get; set; }
        /// <summary>
        /// Departure time for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "depature_time")]
        public string depature_time { get; set; }
        /// <summary>
        /// Arrival time for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "arrival_time")]
        public string arrival_time { get; set; }
        /// <summary>
        /// Fare basis code for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "fare_basis_code")]
        public string fare_basis_code { get; set; }
        /// <summary>
        /// Fare details for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "fare")]
        public Currency fare { get; set; }
        /// <summary>
        /// Tax for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tax")]
        public Currency tax { get; set; }
        /// <summary>
        /// Fees for the current leg of travel
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "fee")]
        public Currency fee { get; set; }
        /// <summary>
        /// Endorsement or transaction
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "endorsement_or_restriction")]
        public string endorsement_or_restriction { get; set; }
    }
}
