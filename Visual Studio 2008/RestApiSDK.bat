call "C:\Program Files (x86)\Microsoft Visual Studio 9.0\Common7\IDE\devenv.com" RestApiSDK\RestApiSDK.sln /build Release

copy /Y RestApiSDK\bin\Release\RestApiSDK.dll RestApiSample\Packages\RestApiSDK\lib. 
copy /Y RestApiSDK\bin\Release\RestApiSDK.xml RestApiSample\Packages\RestApiSDK\lib. 

