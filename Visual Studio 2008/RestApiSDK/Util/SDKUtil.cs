using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace PayPal.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class SDKUtil
    {
        public static string FormatURIPath(string pattern, Object[] parameters)
        {
            if (parameters != null && parameters.Length == 1
					&& parameters[0] is QueryParameters) {

				// Form a object array using the passed Map
				parameters = splitParameters(pattern, ((QueryParameters) parameters[0]).GetMap());
			}

            // Perform a simple message formatting
            string formatString = string.Format(pattern, parameters);

            // Process the resultant string for removing nulls
            return RemoveNullsFromQueryString(formatString);
        }

        private static string RemoveNullsFromQueryString(string formatString)
        {
            if (formatString != null && formatString.Length != 0)
            {
                string[] parts = formatString.Split('?');

                // Process the query string part
                if (parts.Length == 2)
                {
                    string queryString = parts[1];
                    string[] queryStringSplit = queryString.Split('&');
                    if (queryStringSplit.Length > 0)
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (string query in queryStringSplit)
                        {
                            string[] valueSplit = query.Split('=');
                            if (valueSplit.Length == 2)
                            {
                                if (valueSplit[1].Trim().ToLower().Equals("null"))
                                {
                                    continue;
                                }
                                else if (valueSplit[1].Trim().Length == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    builder.Append(query).Append("&");
                                }
                            }
                            else if (valueSplit.Length < 2)
                            {
                                continue;
                            }
                        }
                        formatString = (!builder.ToString().EndsWith("&")) ? builder.ToString()
                            : builder.ToString().Substring(0, builder.ToString().Length - 1);
                    }

                    // append the query string delimiter
                    formatString = (parts[0].Trim() + "?") + formatString;
                }
            }
            return formatString;
        }

         /**
	     * Split the URI and form a Object array using the query string and values
	     * in the provided map. The return object array is populated only if the map
	     * contains valid value for the query name. The object array contains null
	     * values if there is no value found in the map
	     * 
	     * @param pattern
	     *            URI pattern
	     * @param parameters
	     *            Map containing the query name and value
	     * @return Object array
	     */
	    private static Object[] splitParameters(String pattern,
                Dictionary<String, String> parameters)
        {
		    
            List<Object> objectList = new List<Object>();
            String[] query = pattern.Split('?');
            if (query != null && query.Length == 2 && query[1].Contains("={"))
            {
                NameValueCollection queryParts = HttpUtility.ParseQueryString(query[1]);

                foreach (String k in queryParts.AllKeys)
                {
                    string val = "";
                    if (parameters.TryGetValue(k.Trim(), out val))
                    {
                        objectList.Add(val);
                    }                    
                    else
                    {
                        objectList.Add(null);
                    }
				
                }
            }
		    return objectList.ToArray();
	    }
    }
}
