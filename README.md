# PayPal SDK 2.0.0-beta [![Integration Build Status](https://travis-ci.org/paypal/PayPal-NET-SDK.svg?branch=2.0-travis)](https://travis-ci.org/paypal/PayPal-NET-SDK)

This is a preview of the next major version of the PayPal .NET SDK. We've simplified the interface to only provide HTTPRequest that can easily be called via our HttpClient. This SDK is fully .NET Core 2.0 compatible. 

## What's New

Please see the [CHANGELOG.md](./CHANGELOG.md) for the latest changes.

### Creating a Payment

```cs
using System.Linq;
using PayPal.Core;
using PayPal.Payment;

var environment = new SandboxEnvironment("AdV4d6nLHabWLyemrw4BKdO9LjcnioNIOgoz7vD611ObbDUL0kJQfzrdhXEBwnH8QmV-7XZjvjRWn0kg", "EPKoPC_haZMTq5uM9WXuzoxUVdgzVqHyD5avCyVC1NCIUJeVaNNUZMnzduYIqrdw-carG9LBAizFGMyK");
var client = new PayPalHttpClient(environment);

var payment = new Payment()
            {
                Intent = intent,
                Transactions = new List<Transaction>() 
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = "10",
                            Currency = "USD"
                        }
                    }
                },
                RedirectUrls = new RedirectUrls() 
                {
                    CancelUrl = "https://example.com/cancel",
                    ReturnUrl = "https://example.com/return"
                }
                Payer = new Payer() 
                {
                    PaymentMethod = "paypal"
                }
            };

PaymentCreateRequest request = new PaymentCreateRequest();
request.RequestBody(payment);

try 
{
  HttpResponse response = await client.Execute(request);
  var statusCode = response.StatusCode;
  Payment result = response.Result<Payment>();
} 
catch(HttpException httpException) 
{
  var statusCode = httpException.StatusCode;
  var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
}
```

If you're migrating from v1, check out our [Migration Guide](./docs/Migrating.md) or our [Frequently Asked Questions](./docs/FAQ.md).

## Building

To try this out, update the version of `paypal-rest-sdk` by running:
```sh
$ dotnet add package PayPal -v 2.0.0-rc2
```

Please feel free to create an issue in this repo with any feedback, questions, or concerns you have.

## Running tests

To run integration tests, clone this repository and run the following command:
```sh
$ dotnet test -v normal
```

*NOTE*: This API is still in beta, is subject to change, and should not be used in production.

## Feedback

If you have any suggestions/remarks/feedback related to SDK 2.0.0, feel free to create an issue.

## License
PayPal-NET-SDK is open source. See the [LICENSE](./LICENSE) file for more info.

## Contributions
Pull requests and new issues are welcome. See [CONTRIBUTING.md](CONTRIBUTING.md) for details.
