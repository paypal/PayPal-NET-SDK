# PayPal .NET SDK

![Home Image](https://raw.githubusercontent.com/wiki/paypal/PayPal-NET-SDK/images/homepage.jpg)

[![Build Status](https://ci.appveyor.com/api/projects/status/github/paypal/paypal-net-sdk?svg=true)](https://ci.appveyor.com/project/paypal/paypal-net-sdk) [![NuGet](https://img.shields.io/nuget/v/PayPal.svg)](https://www.nuget.org/packages/PayPal)

The PayPal .NET SDK makes it easy to add PayPal support to your .NET web application and is built on [PayPal's REST APIs](https://developer.paypal.com/docs/api/).

## TLSv1.2 Update
> **The Payment Card Industry (PCI) Council has [mandated](http://blog.pcisecuritystandards.org/migrating-from-ssl-and-early-tls) that early versions of TLS be retired from service.  All organizations that handle credit card information are required to comply with this standard. As part of this obligation, PayPal is updating its services to require TLS 1.2 for all HTTPS connections. At this time, PayPal will also require HTTP/1.1 for all connections. [Click here](https://github.com/paypal/tls-update) for more information**

> **Connections to the sandbox environment use only TLS 1.2.**

## PayPal Checkout v2
Please note that if you are integrating with PayPal Checkout, this SDK and corresponding API [v1/payments](https://developer.paypal.com/docs/api/payments/v1/) are in the process of being deprecated.

We recommend that you integrate with API [v2/checkout/orders](https://developer.paypal.com/docs/api/orders/v2/) and [v2/payments](https://developer.paypal.com/docs/api/payments/v2/). Please refer to the [Checkout .NET SDK](https://github.com/paypal/Checkout-NET-SDK) to continue with the integration.

## 2.0 Release Candidate!
We're releasing a [brand new version of our SDK!](https://github.com/paypal/PayPal-net-SDK/tree/2.0-beta) 2.0 is currently at release candidate status, and represents a full refactor, with the goal of making all of our APIs extremely easy to use. 2.0 includes all of the existing APIs (except payouts), and includes the new Orders API (Disputes and Marketplace coming soon). Check out the [FAQ and migration guide](https://github.com/paypal/PayPal-net-SDK/tree/2.0-beta/docs), and let us know if you have any suggestions or issues!

## Prerequisites

* .NET 4.0 or later

## Documentation

* [SDK Home Page](http://paypal.github.io/PayPal-NET-SDK/) - A great starting place for using this SDK; includes links to the wiki, sample project source code, releases, and more!
* [SDK Wiki](https://github.com/paypal/PayPal-NET-SDK/wiki)
  * [Getting Started](https://github.com/paypal/PayPal-NET-SDK/wiki#getting-started) - Everything you need to begin using this SDK.
  * [Quick Start](https://github.com/paypal/PayPal-NET-SDK/wiki/Quick-Start) - For those looking to hit the ground running!
  * [Samples](https://github.com/paypal/PayPal-NET-SDK/wiki/Samples)
  * [Classic SDK Compatibility](https://github.com/paypal/PayPal-NET-SDK/wiki/Classic-SDK-Compatibility)
  * [FAQ](https://github.com/paypal/PayPal-NET-SDK/wiki/Frequently-Asked-Questions)
  * [Contributing to the SDK](https://github.com/paypal/PayPal-NET-SDK/wiki/Contributing-to-the-SDK)

General documentation regarding the PayPal REST API and related payment flows can be found on the [PayPal Developer](https://developer.paypal.com/) site. 

* [PayPal Developer Documentation](https://developer.paypal.com/docs/)
* [PayPal REST API Reference](https://developer.paypal.com/webapps/developer/docs/api/)

## License

* PayPal, Inc. SDK License - [LICENSE.txt](https://github.com/paypal/PayPal-NET-SDK/blob/master/LICENSE.txt)

[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/paypal/PayPal-NET-SDK/trend.png)](https://bitdeli.com/free "Bitdeli Badge")
