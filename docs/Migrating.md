# Migrating from Version 1 to Version 2

## 1. Initialize SDK

#### BEFORE
You would add this global configuration to your app.config file or web.config file.
```xml
<configuration>
  <configSections>
    <section name="paypal" type="PayPal.SDKConfigHandler, PayPal" />
  </configSections>

  <!-- PayPal SDK settings -->
  <paypal>
    <settings>
      <add name="mode" value="sandbox" />
      <add name="clientId" value="__CLIENT_ID__" />
      <add name="clientSecret" value="__CLIENT_SECRET__" />
    </settings>
  </paypal>
</configuration>
```

additionally, you would setup your API context like so:

```cs
using PayPal.Api;

// Get a reference to the config
var config = ConfigManager.Instance.GetProperties();

// Use OAuthTokenCredential to request an access token from PayPal
var accessToken = new OAuthTokenCredential(config).GetAccessToken();

var apiContext = new APIContext(accessToken);
```

#### AFTER
```js
using System.Linq;
using PayPal.Core;
using PayPal.PaymentExperience;

// Sandbox Environment
var environment = new SandboxEnvironment("AdV4d6nLHabWLyemrw4BKdO9LjcnioNIOgoz7vD611ObbDUL0kJQfzrdhXEBwnH8QmV-7XZjvjRWn0kg", "EPKoPC_haZMTq5uM9WXuzoxUVdgzVqHyD5avCyVC1NCIUJeVaNNUZMnzduYIqrdw-carG9LBAizFGMyK");
// Live Environment
// var environment = new LiveEnvironment("<client-id>", "<client-secret>");

var client = new PayPalHttpClient(environment);
```

# 2. Make a call

#### BEFORE
```cs
var profile = new WebProfile()
            {
                name = Guid.NewGuid().ToString(),
                presentation = new Presentation()
                {
                    brand_name = "Sample brand",
                    locale_code = "US",
                    logo_image = "https://www.paypal.com/",
                    note_to_seller_label = "Thx",
                    return_url_label = "Retreat!"
                },
                input_fields = new InputFields()
                {
                    address_override = 1,
                    allow_note = true,
                    no_shipping = 0
                },
                flow_config = new FlowConfig()
                {
                    bank_txn_pending_url = "https://www.paypal.com/",
                    landing_page_type = "billing",
                    user_action = "commit",
                    return_uri_http_method = "GET"
                },
                temporary = true
            };

            // Create the profile
            var response = profile.Create(apiContext);
```

#### AFTER
```cs
WebProfileCreateRequest request = new WebProfileCreateRequest();
var profile = new WebProfile()
            {
                Name = "some_name",
                Presentation = new Presentation()
                {
                    BrandName = "Sample brand",
                    LocaleCode = "US",
                    LogoImage = "https://www.paypal.com/",
                    NoteToSellerLabel = "Thx",
                    ReturnUrlLabel = "Retreat!"
                },
                InputFields = new InputFields()
                {
                    AddressOverride = 1,
                    AllowNote = true,
                    NoShipping = 0
                },
                FlowConfig = new FlowConfig()
                {
                    BankTxnPendingUrl = "https://www.paypal.com/",
                    LandingPageType = "billing",
                    UserAction = "commit",
                    ReturnUriHttpMethod = "GET"
                },
                Temporary = true
            };

request.RequestBody(profile);

try 
{
  HttpResponse response = await client.Execute(request);
  var statusCode = response.StatusCode;
  WebProfile result = response.Result<WebProfile>();
} 
catch(BraintreeHttp.HttpException httpException) 
{
  var statusCode = httpException.StatusCode;
  var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
}
```

