.NET PayPal REST API Sample
===========================


Overview
--------
The Sample showcases the features of PayPal REST APIs
*	Save Credit Card with PayPal for later Payments
*	Make Payments using the saved Credit Card ID
*	Make Payments using PayPal as the Payment Method


Pre-requisites
--------------
*	Visual Studio 2008 or higher
*	Nuget 2.2 
	*	Note: NuGet 2.2 requires .NET Framework 4.0


Dependent library references
----------------------------
*	RestApiSDK
*	PayPalCoreSDK.dll
*	Newtonsoft.Json.dll
*	log4net.dll

	
SDK Integration
---------------
*	Integrate PayPal REST API SDK with an ASP.NET Web Application

*	Use NuGet.exe to install the dependencies in Visual Studio 2008

*	The NuGet package installs the dependencies to the solution and automatically updates the project in Visual Studio 2010


NuGet - Installing NuGet in Visual Studio 2010 and 2012
-------------------------------------------------------

Go to Visual Studio 2010 Menu --> Tools
Select Extension Manager
Enter NuGet in the search box and click Online Gallery
Let it Retrieve information
Select the retrieved NuGet Package Manager, click Download
Let it Download
Click Install on the Visual Studio Extension Installer NuGet Package Manager
Let it Install
Click Close and Restart Now

Go to Visual Studio 2010 Menu --> Tools, select Options
Verify the following on the Options popup
Click Package Manager --> Package Sources
Available package sources:
Check box (checked) NuGet official package source
https://nuget.org/api/v2/
Name: NuGet official package source
Source: https://nuget.org/api/v2/
And click OK
 
Go to Menu --> Tools --> Library Package Manage --> Package Manager Console
Select NuGet official package source from the Package source dropdown box in the Package Manager Console
Go to Solution Explorer and note the existing references
Enter at PM>
*******************************************

*	PM>Install-Package PayPalCoreSDK -excludeversion
	*	PayPalCoreSDK.dll
	*	log4net.dll
*	PM>Install-Package Newtonsoft.Json
	*	Newtonsoft.Json.dll
*	Note that the refrences get added automatically	
	
*******************************************

	
NuGet - Integrating NuGet with Visual Studio 2008
-------------------------------------------------

Prerequisites:
*	.NET Framework 4.0
*	NuGet.exe
	
Check if .NET Framework 4.0 is installed in the computer from Control Panel --> Get programs

Or run the following command from Windows command prompt:
>dir  /b  %windir%\Microsoft.NET\Framework\v*

Running the aforesaid command should list the .NET Framework versions installed as follows:
*	v1.0.3705
*	v1.1.4322
*	v2.0.50727
*	v3.0
*	v3.5
*	v4.0.30319

Note: Most Windows machines would have .NET Framework 4.0 installed as part of Windows (recommended) update.

If V4.X is not installed, then download and install
	.NET Framework 4 (Standalone Installer) (free to download):
http://www.microsoft.com/en-in/download/details.aspx?id=17718

Or

	.NET Framework 4 (Web Installer) (free to download):
http://www.microsoft.com/en-in/download/details.aspx?id=17851

Download NuGet.exe Command Line (free to download): http://nuget.codeplex.com/releases/view/58939

Save NuGet.exe to folder viz., 'C:\NuGet' and add its path to the Environment Variables Path:

Visual Studio 2005 or 2008
Go to Visual Studio Menu --> Tools
Select External Tools
External Tools
External Tools having 5* default tools in the Menu contents, Click Add
*Note: The number of default tools may differ depending on the particular Visual Studio installation
 
Enter the following:
Title: NuGet Install
Command (Having in Environment Variables Path): NuGet.exe
Arguments: install your.package.name -excludeversion -outputDirectory .\Packages
Initial directory: $(SolutionDir)
Use Output window: Check
Prompt for arguments: Check
Click Apply
Click OK

On Clicking Apply and OK, NuGet Install will be added (as External Command 6*) to Menu --> Tools
*Note: The External Command number may differ depending on the particular Visual Studio installation

Menu --> Tools, clicking NuGet Install will pop up for NuGet Install Arguments and Command Line
Also, NuGet Toolbar can be added, right-click on Visual Studio Menu and select Customize
Customize by clicking New
Enter Toolbar name: NuGet and click OK
Check NuGet Checkbox in the Toolbars tab for NuGet Toolbar to pop up
Click Commands tab and select Tools and External Command 6 (Having added NuGet Install as External Command 6*) 
*Note: The External Command number may differ depending on the particular Visual Studio installation
Drag and drop External Command 6 to NuGet Toolbar
Right-click NuGet Toolbar
Enter Name: Install Package
Right-click Change Button Image and select an image
Close Customize
Drag and drop NuGet Toolbar to the Menu
Click the NuGet Toolbar Install Package
Clicking on the NuGet Toolbar Install Package will pop up for NuGet Install Arguments and Command Line
Example NuGet Install:
Enter Arguments: 
*******************************************
install RestApiSDK -excludeVersion -outputDirectory .\Packages

install PayPalCoreSDK -excludeversion -outputDirectory .\Packages
		
install Newtonsoft.Json -excludeversion -outputDirectory .\Packages
	
*******************************************
	
Namespaces
----------
*	PayPal
*	PayPal.Manager
*	PayPal.Api.Payments
	
References
----------

*	REST API SDK repository - https://github.com/paypal/rest-api-sdk-dotnet
