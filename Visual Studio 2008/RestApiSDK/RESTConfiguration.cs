using System;
using System.Collections.Generic;
using System.Text;

namespace PayPal
{
    /// <summary>
    /// REST Configuration
    /// </summary>
    public class RESTConfiguration
    {
        /// <summary>
        /// Gets or sets the authorization token.
        /// </summary>
        /// <value>
        /// The authorization token.
        /// </value>
        public string authorizationToken
        {
            get;
            set;
        }

        /// <summary>
        /// The request identity
        /// </summary>
        private string requestIdentity;
        /// <summary>
        /// Gets or sets the request id.
        /// </summary>
        /// <value>
        /// The request id.
        /// </value>
        public string requestId
        {
            private get
            {
                return requestIdentity;
            }
            set
            {
                requestIdentity = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RESTConfiguration"/> class.
        /// </summary>
        public RESTConfiguration() {}

        /// <summary>
        /// Gets the current header tags and adds tags for; Authorization, User-Agent, and PayPal-Request-Id.
        /// </summary>
        /// <returns>The header information as a Dictionary of string, string</returns>
        public  Dictionary<string, string> GetHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", authorizationToken);
            headers.Add("User-Agent", FormUserAgentHeader());
            headers.Add("PayPal-Request-Id", requestId);
            return headers;
        }

        /// <summary>
        /// Forms the user agent header.
        /// </summary>
        /// <returns>The operating system informational header tag</returns>
        private string FormUserAgentHeader()
        {
            string header = null;
            StringBuilder stringBuilder = new StringBuilder("PayPalSDK/"
                    + PayPalResource.SdkID + " " + PayPalResource.SdkVersion
                    + " ");
            string dotNETVersion = GetDotNetVersionHeader();
            stringBuilder.Append(";").Append(dotNETVersion);
            string osVersion = GetOSHeader();
            if (osVersion.Length > 0)
            {
                stringBuilder.Append(";").Append(osVersion);
            }
            header = stringBuilder.ToString();
            return header;
        }

        /// <summary>
        /// Gets the operating system header.
        /// </summary>
        /// <returns>The operating system informational header tag</returns>
        private string GetOSHeader()
        {
            string osHeader = string.Empty;
            if (JCS.OSVersionInfo.OSBits.Equals(JCS.OSVersionInfo.SoftwareArchitecture.Bit64))
            {
                osHeader += "bit=" + 64 + ";";
            }
            else if (JCS.OSVersionInfo.OSBits.Equals(JCS.OSVersionInfo.SoftwareArchitecture.Bit32))
            {
                osHeader += "bit=" + 32 + ";";
            }
            else
            {
                osHeader += "bit=" + "Unknown" + ";";
            }

            osHeader += "OS=" + JCS.OSVersionInfo.Name + " " + JCS.OSVersionInfo.Version + ";";
            return osHeader;
        }

        /// <summary>
        /// Gets the dot net version header tag.
        /// </summary>
        /// <returns>The dot net version header tag as string</returns>
        private string GetDotNetVersionHeader()
        {
            string DotNetVersionHeader = "lang=" + "DOTNET;" + "V=" + Environment.Version.ToString().Trim();
            return DotNetVersionHeader;
        }          
    }
}
