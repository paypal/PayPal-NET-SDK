.NET PayPal REST API Sample
===========================


Pre-requisites
--------------
   
   * Visual Studio 2012 (.NET Framework 4.5) or higher
		Or
   * Visual Studio 2010 (.NET Framework 4.0)
		Or
   * Visual Studio 2008 (.NET Framework 3.5)
   * NuGet 2.2 or higher in case of NuGet Install [Note: NuGet requires .NET Framework 4.0 or higher]


Please note: bin and obj folders
--------------------------------

   * Please delete the bin and obj folders before switching between different versions of Visual Studio 


Pre-requisites
--------------
*	Visual Studio 2008 or Visual Studio 2010 or Visual Studio 2012
*	NuGet 2.2 or higher [NuGet requires .NET Framework 4.0 or higher]


Dependent library references
----------------------------
*	RestApiSDK
*	PayPalCoreSDK.dll
*	log4net.dll
*	Newtonsoft.Json.dll

	
SDK Integration
---------------
*	Integrate PayPal REST API SDK with an ASP.NET Web Application

*	Use NuGet.exe to install the dependencies in Visual Studio 2008

*	The NuGet package installs the dependencies and adds references automatically to the project in Visual Studio 2010 and Visual Studio 2012


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
******************************************************************

*	PM>Install-Package RestApiSDK
*	Note that the following references get added automatically  	
	*	RestApiSDK.dll
	*	PayPalCoreSDK.dll
	*	log4net.dll
	*	Newtonsoft.Json.dll	

******************************************************************

	
NuGet - Integrating NuGet with Visual Studio 2008
-------------------------------------------------

Prerequisites:
*	NuGet 2.2 or higher [NuGet requires .NET Framework 4.0 or higher]
	
Check if .NET Framework 4.0 is installed in the computer from Control Panel --> Get programs

Or else

Run the following command from Windows Command Prompt:
wmic product where "Name like 'Microsoft .Net%'" get Name, Version
	
*	Running the aforesaid command should list the .NET Framework versions installed as in this particular case 
*	[Please note the command may take a while to execute]

Name                                                Version
Microsoft .NET Compact Framework 1.0 SP3 Developer  1.0.4292
Microsoft .NET Framework 4.5                        4.5.50709
Microsoft .NET Framework 4.5 Multi-Targeting Pack   4.5.50709
Microsoft .NET Framework 2.0 SDK (x64) - ENU        2.0.50727
Microsoft .NET Framework 4 Multi-Targeting Pack     4.0.30319
Microsoft .NET Framework 4.5 SDK                    4.5.50709
Microsoft .NET Compact Framework 2.0 SP2            2.0.7045
Microsoft .NET Compact Framework 3.5                3.5.7283
Microsoft .NET Framework 1.1                        1.1.4322
Microsoft .NET Compact Framework 1.0 SP3            1.0.4294

Note: Most Windows machines may have .NET Framework 4.0 or higher installed as part of Windows (Recommended) Update

If V4.X is not installed, then download and install

	.NET Framework 4 (Standalone Installer) (free to download from http://www.microsoft.com/)
Or

	.NET Framework 4 (Web Installer) (free to download from http://www.microsoft.com/)

Download NuGet.exe Command Line (free to download): http://nuget.codeplex.com/releases/

Save NuGet.exe to a folder viz., 'C:\NuGet' and add its path to the Environment Variables Path:

Visual Studio 2008
Go to Menu -> Tools

Select External Tools

External Tools

External Tools having 5 default tools in the Menu contents
*	Note: The number of default tools may differ depending on the particular Visual Studio installation

Click Add

Enter the following:
Title: NuGet Install
Command (Having set the Environment Variables Path): NuGet.exe
Arguments: install your.package.name -outputDirectory .\packages

Ensure the following:
Initial directory: Select Solution Directory i.e., $(SolutionDir)
Use Output window: Check
Prompt for arguments: Check

Click Apply

Click OK

On Clicking Apply and OK, let the NuGet Install be added (as External Command 6) to Menu -> Tools
*	Note: The External Command number may differ depending on the particular Visual Studio installation

Menu -> Tools, clicking NuGet Install will pop up for NuGet Install Arguments and Command Line

Also, let the NuGet Toolbar be added to Visual Studio
Right-click on Visual Studio Menu and select Customize

Customize by clicking New

Enter Toolbar name: NuGet 
Click OK

Check NuGet Checkbox in the Toolbars tab for NuGet Toolbar to pop up

Click Commands tab and select Tools and External Command 6 (Having added NuGet Install as External Command 6) 
*	Note: The External Command number may differ depending on the particular Visual Studio installation

Drag and drop External Command 6 to NuGet Toolbar

Right-click NuGet Toolbar

Enter Name: Install Package

Right-click Change Button Image and select an image

Close Customize

Drag and drop the NuGet Toolbar to the Menu

Click the NuGet Toolbar Install Package

Clicking on the NuGet Toolbar Install Package will pop up for NuGet Install Arguments and Command Line

Enter Arguments: 

**********************************************
install RestApiSDK -OutputDirectory .\packages
	
**********************************************

On clicking OK, the output window should display: "Successfully installed"

Menu View -> Output (Ctrl+Alt+O)
 
After successful installation, note that the dependencies are downloaded to the 'packages' folder which resides in the same folder as that of the Solution (.sln) file

Add the references to the dependencies downloaded, from the 'packages' folder

	
Namespaces
----------
*	PayPal
*	PayPal.Manager
*	PayPal.Api.Payments
	
References
----------

*	REST API SDK repository - https://github.com/paypal/rest-api-sdk-dotnet
