using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PayPal.Api
{
    /// <summary>
    /// Represents a PayPal model object that will be returned from PayPal containing common resource data.
    /// </summary>
    public class PayPalRelationalObject : PayPalResource
    {
        /// <summary>
        /// A list of HATEOAS (Hypermedia as the Engine of Application State) links.
        /// More information: https://developer.paypal.com/docs/api/#hateoas-links
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "links")]
        public List<Links> links { get; set; }

        /// <summary>
        /// Gets the HATEOAS link that matches the specified relation name.
        /// </summary>
        /// <param name="relationName">The name of the link relation.</param>
        /// <returns>A Links object containing the details of the HATEOAS link; null if not found.</returns>
        public Links GetHateoasLink(string relationName)
        {
            foreach (var link in this.links)
            {
                if (link.rel.Equals(relationName))
                {
                    return link;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the approval URL from a list of HATEOAS links.
        /// </summary>
        /// <param name="setUserActionParameter">If true, appends the 'useraction' URL query parameter.
        /// <para>For PayPal payments, this will set the approval button text on the PayPal site to "Pay Now".</para></param>
        /// <returns>The approval URL or an empty string if not found.</returns>
        public string GetApprovalUrl(bool setUserActionParameter = false)
        {
            var link = this.GetHateoasLink(BaseConstants.HateoasLinkRelations.ApprovalUrl);
            if (link != null)
            {
                return link.href + (setUserActionParameter ? "&useraction=commit" : "");
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the resource token from an approval URL HATEOAS link, if found.
        /// </summary>
        /// <returns>A string containing the resource token associated with an approval URL.</returns>
        public string GetTokenFromApprovalUrl()
        {
            var approvalUrl = this.GetApprovalUrl();
            if (!string.IsNullOrEmpty(approvalUrl))
            {
                return HttpUtility.ParseQueryString((new Uri(approvalUrl)).Query).Get("token");
            }
            return string.Empty;
        }
    }
}
