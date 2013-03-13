The repository contains the PayPal PaymentSDK C#.NET Class Library Application and the PaymentSDKUnitTest C#.NET Visual Studio Test Application.


Prerequisites
-------------
   *   Visual Studio 2008 or higher
   *   log4net 1.2.10
   *   NuGet 2.2
   *   Note: NuGet 2.2 requires .NET Framework 4.0


The PayPal PaymentSDK for .NET
----------------------------

	
To make an API call
--------------------
   *   Add references to 
   *   RestApiSDK 0.5.0.0 
   *   PayPalCoreSDK.dll 
   *   Newtonsoft.Json.dll
   *   log4net.dll 1.2.10.0
   *   Refer to the App.config file configuration file settings		
   *   Create 'accesstoken' from 'ClientID' and 'ClientSecret' using 'OAuthTokenCredential' and set the same in resource as follows,

		// ###AccessToken
		// Retrieve the access token from
		// OAuthTokenCredential by passing in
		// ClientID and ClientSecret
		// It is not mandatory to generate Access Token on a per call basis.
		// Typically the access token can be generated once and
		// reused within the expiry window
		string accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperty("ClientID"), ConfigManager.Instance.GetProperty("ClientSecret")).GetAccessToken();
		
   *   Depending on the context of API calls, calling method may be static or non-static (For example, most 'GET' http methods are created as `static` methods within the resource).
	 * If it is static, invoke it as a class method as like

		// Retrieve the payment object by calling the
		// static `Get` method
		// on the Payment class by passing a valid
		// AccessToken and Payment ID
		Payment pymnt = Payment.Get(accessToken, "PAY-0XL713371A312273YKE2GCNI");
			
	 * If it is non-static, invoke it using resource object as like

		// ###Payment
		// A Payment Resource; create one using
		// the above types and intent as `sale`
		Payment pymnt = new Payment();
		pymnt.intent = "sale";
		...
		...
		...
		pymnt.Create(accessToken);

			
App.Config Configuration:
------------------
   *   endpoint
   *   ClientID
   *   ClientSecret


License
-------
   *   PayPal, Inc. SDK License - LICENSE.txt


log4net 1.2.10 using NuGet
--------------------------
   *   Visual Studio 2010 and 2012:
        Go to Menu --> Tools --> Library Package Manager --> Package Manager Console
	Select NuGet official package source from the Package source dropdown box in the Package Manager Console
	Enter at PM> 
	Install-Package <package.name> -Version 1.2.10
	
   *   Visual Studio 2008
	NuGet Install Arguments: 
	install <package.name> -excludeversion -outputDirectory .\Packages


NuGet 2.2
---------
   *	NuGet is a Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects that use the .NET Framework. 
	If you develop a library or tool that you want to share with other developers, you create a NuGet package and store the package in a NuGet repository. 
	If you want to use a library or tool that someone else has developed, you retrieve the package from the repository and install it in your Visual Studio project or solution. 
	When you install the package, NuGet copies files to your solution and automatically makes whatever changes are needed, such as adding references and changing your app.config or web.config file. 
	If you decide to remove the library, NuGet removes files and reverses whatever changes it made in your project so that no clutter is left.


NuGet - Installing NuGet in Visual Studio 2012 and 2010
-------------------------------------------------------
Go to Menu -> Tools

Select Extensions and Updates (Extension Manager... in Visual Studio 2010)

Enter NuGet in the search box and click Online (Online Gallery in Visual Studio 2010)

Let it Retrieve information

Select the retrieved NuGet Package Manager, and click Download

Let it Download

Click Install on the VSIX (Visual Studio Extension in Visual Studio 2010) Installer NuGet Package Manager

Let it do Installing

Click Close

Click Restart Now

Go to Menu -> Tools, select Options

Verify the following on the Options pop-up
Click Package Manager -> Package Sources
Available package sources:
Check box (checked) NuGet official package source
https://nuget.org/api/v2/
Name: NuGet official package source
Source: https://nuget.org/api/v2/
Click OK

Go to Menu -> Tools -> Library Package Manage -> Package Manager Console

Select NuGet official package source from the Package source dropdown box in the Package Manager Console

Go to Solution Explorer and note the existing references

Enter at PM> Install-Package PayPalCoreSDK

After successful installation, note that the new references get added automatically

Also, note that the dependencies are downloaded and installed to the Packages folder of the project directory - Select the project and click Show All Files, and expand the Packages folder

If the Packages folder was not included in the project - Select the project, click Show All Files, and expand the packages folder

Also, go to Menu -> Tools -> Library Package Manager, select Manage NuGet Packages for Solution

Manage NuGet Packages
log4net
PayPal Core SDK for .NET

Also on Manage NuGet Packages, search for PayPal

Caution: Need to have a solution open for Install-Package

Install-Package : The current environment doesn't have a solution open.

PayPal Packages in NuGet Gallery

 
NuGet - Integrating NuGet with Visual Studio 2008 and 2005
----------------------------------------------------------
Prerequisites:
   *   .NET Framework 4.0 or higher
   *   NuGet 2.2
	

Check if .NET Framework 4.0 or higher is installed in the Computer from Control Panel -> Get programs

Or else

Run the following command from Windows Command Prompt:
>dir  /b  %windir%\Microsoft.NET\Framework\v*

Running the aforesaid command should list the .NET Framework versions installed as follows:
   * v1.0.3705
   * v1.1.4322
   * v2.0.50727
   * v3.0
   * v3.5
v4.0.30319

Note: Most Windows machines may have .NET Framework 4.0 or higher installed as part of Windows (Recommended) Update.

If V4.X is not installed, then download and install

	.NET Framework 4 (Standalone Installer)  (free to download):
	http://www.microsoft.com/en-in/download/details.aspx?id=17718

Or else

	.NET Framework 4 (Web Installer) (free to download):
	http://www.microsoft.com/en-in/download/details.aspx?id=17851


Download NuGet.exe Command Line (free to download): http://nuget.codeplex.com/releases/view/58939

Save NuGet.exe to a folder viz., 'C:\NuGet' and add its path to the Environment Variables Path:

Visual Studio 2008
Go to Menu -> Tools

Select External Tools

External Tools

External Tools having 5* default tools in the Menu contents
*Note: The number of default tools may differ depending on the particular Visual Studio installation

Click Add

Enter the following:
Title: NuGet Install
Command (Having set the Environment Variables Path): NuGet.exe
Arguments: install your.package.name -excludeversion -outputDirectory .\Packages

Ensure the following:
Initial directory: Select Project Directory i.e., $(ProjectDir)
Use Output window: Check
Prompt for arguments: Check

Click Apply

Click OK

On Clicking Apply and OK, let the NuGet Install be added (as External Command 6*) to Menu > Tools
*Note: The External Command number may differ depending on the particular Visual Studio installation

Menu > Tools, clicking NuGet Install will pop up for NuGet Install Arguments and Command Line

Also, let the NuGet Toolbar be added to Visual Studio
Right-click on Visual Studio Menu and select Customize

Customize by clicking New

Enter Toolbar name: NuGet 
Click OK

Check NuGet Checkbox in the Toolbars tab for NuGet Toolbar to pop up

Click Commands tab and select Tools and External Command 6 (Having added NuGet Install as External Command 6*) 
*Note: The External Command number may differ depending on the particular Visual Studio installation

Drag and drop External Command 6 to NuGet Toolbar

Right-click NuGet Toolbar

Enter Name: Install Package

Right-click Change Button Image and select an image

Close Customize

Drag and drop the NuGet Toolbar to the Menu

Click the NuGet Toolbar Install Package

Clicking on the NuGet Toolbar Install Package will pop up for NuGet Install Arguments and Command Line

NuGet Install: PayPalCoreSDK

Enter Arguments: 
install PayPalCoreSDK -excludeversion -outputDirectory .\Packages
 
Menu View -> Output (Ctrl+Alt+O)
 
After successful installation, note that the dependencies are downloaded and installed to the Packages folder of the Project Directory i.e., $(ProjectDir) - Select the project and click Show All Files, and expand the Packages folder

If the Packages folder was not included in the project - Select the project, click Show All Files, and expand the Packages folder

Add the references to the dependencies downloaded and installed to the Packages folder

Caution:
If Solution got selected during NuGet Install, the package will not be installed to Project Directory i.e., $(ProjectDir), instead it will be downloaded to the Visual Studio IDE folder for which access may be denied depending on the folder permissions

Access to the path, 'C:\Program Files (x86)\Microsoft Visual Studio 8\Common7\IDE\Packages\log4net\lib\1.0' is denied.

The package installed to wrong folder, 'C:\Program Files (x86)\Microsoft Visual Studio 8\Common7\IDE'

NuGet Install without loading any project will be denied access as Initial directory has been set as Project Directory i.e., $(ProjectDir)

Attempting to resolve dependency 'PayPalCoreSDK'.
Attempting to resolve dependency 'log4net (1.2.10)'.
Access to the path 'C:\Program Files (x86)\Microsoft Visual Studio 8\Common7\IDE\Packages\log4net\lib\1.0' is denied.