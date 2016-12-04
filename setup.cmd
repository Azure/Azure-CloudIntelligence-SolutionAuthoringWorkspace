@ECHO OFF

SET MSBUILD_PATH=D:\Program Files (x86)\MSBuild\14.0\Bin
SET NUGET_PATH=D:\Program Files (x86)\SiteExtensions\Kudu\59.51202.2583\bin\Scripts
SET PATH=%NUGET_PATH%;%MSBUILD_PATH%;%PATH%

CALL :verify_command git || exit /b 1
CALL :verify_command nuget || exit /b 1
CALL :verify_command msbuild || exit /b 1

PUSHD Source
nuget restore
POPD

msbuild Source/SolutionAuthoringWorkbench.sln /p:Configuration=Release 

:verify_command
where "%~1" >nul 2>nul
IF %errorlevel%==1 (
	ECHO ERROR: %~1 could not be found.
	EXIT /B 1
)
