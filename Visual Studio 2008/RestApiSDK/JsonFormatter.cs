using System;
// install Newtonsoft.Json -excludeversion -outputDirectory .\Packages
// net35
using Newtonsoft.Json;

namespace PayPal
{
    /// <summary>
    /// Json Formatter
    /// </summary>
    public static class JsonFormatter
    {
        /// <summary>
        /// Convert to json.
        /// </summary>
        /// <typeparam name="T">The parameter type</typeparam>
        /// <param name="t">The parameter.</param>
        /// <returns>The Serialized Object</returns>
        public static string ConvertToJson<T>(T t) 
        {
            return JsonConvert.SerializeObject(t);
        }

        /// <summary>
        /// Convert from json.
        /// </summary>
        /// <typeparam name="T">The parameter type</typeparam>
        /// <param name="response">The json encoded response.</param>
        /// <returns>The Deserialized Object</returns>
        public static T ConvertFromJson<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
