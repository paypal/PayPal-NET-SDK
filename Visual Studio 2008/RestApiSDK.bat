call "C:\Program Files (x86)\Microsoft Visual Studio 9.0\Common7\IDE\devenv.com" RestApiSDK\PayPal.RestApiSDK.sln /build Release

copy /Y RestApiSDK\bin\Release\PayPal.RestApiSDK.dll RestApiSample\Packages\PayPal.RestApiSDK\lib. 
copy /Y RestApiSDK\bin\Release\PayPal.RestApiSDK.xml RestApiSample\Packages\PayPal.RestApiSDK\lib. 

