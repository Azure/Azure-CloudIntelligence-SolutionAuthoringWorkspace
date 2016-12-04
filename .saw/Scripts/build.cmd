@ECHO OFF

SET MSBUILD_PATH=D:\Program Files (x86)\MSBuild\14.0\Bin
SET NUGET_PATH=D:\Program Files (x86)\SiteExtensions\Kudu\59.51202.2583\bin\Scripts
SET PATH=%cd%;%NUGET_PATH%;%MSBUILD_PATH%;%PATH%
SET ENV_ROOT=%~dp0..\..
SET SOURCE_PATH=%ENV_ROOT%\.saw\Source

CALL :verify_command nuget || exit /b 1
CALL :verify_command msbuild || exit /b 1

PUSHD %SOURCE_PATH%
ECHO Running NuGet restore...
nuget restore >nul
ECHO Done!
POPD

ECHO Building SAW tools from the source code...
msbuild %SOURCE_PATH%\SolutionAuthoringWorkbench.sln /p:Configuration=Release >nul
ECHO Done!

:verify_command
where "%~1" >nul 2>nul
IF %errorlevel%==1 (
	ECHO ERROR: %~1 could not be found.
	EXIT /B 1
)
