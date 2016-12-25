SET MSBUILD_PATH=D:\Program Files (x86)\MSBuild\14.0\Bin
SET NUGET_PATH=D:\Program Files (x86)\SiteExtensions\Kudu\59.51202.2583\bin\Scripts
IF NOT EXIST "%MSBUILD_PATH%" GOTO :not_app_service
IF NOT EXIST "%NUGET_PATH%" GOTO :not_app_service
ECHO Looks like this is an App Service environment. Updating PATH appropriately.
SET PATH=%cd%;%NUGET_PATH%;%MSBUILD_PATH%;%PATH%
SET SAW_ROOT=%~dp0..\..
SET PACKAGES_DIRECTORY=%SAW_ROOT%\.saw\NuGetPackages