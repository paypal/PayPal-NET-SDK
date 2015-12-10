using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPal.Util
{
    /// <summary>
    /// Helper class that can be converted into a URL query string.
    /// </summary>
    internal class QueryParameters : Dictionary<string, string>
    {
        /// <summary>
        /// Converts the dictionary of query parameters to a URL-formatted string. Empty values are ommitted from the parameter list.
        /// </summary>
        /// <returns>A URL-formatted string containing the query parameters</returns>
        public string ToUrlFormattedString()
        {
            return this.Aggregate
            (
                "",
                (parameters, item) =>
                    parameters + (string.IsNullOrEmpty(item.Value) ? "" : ((string.IsNullOrEmpty(parameters) ? "?" : "&") + string.Format("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value))))
            );
        }
    }
}