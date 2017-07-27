@echo off
set NUGET_COMMAND=nuget                          
set APPSERVICE_NUGET_PATH=D:\Program Files (x86)\SiteExtensions\Kudu\63.60712.2926\bin\Scripts\nuget.exe

IF EXIST %APPSERVICE_NUGET_PATH% (
	set NUGET_COMMAND="%APPSERVICE_NUGET_PATH%"
)
@echo on
%NUGET_COMMAND% restore -SolutionDirectory ./
