@ECHO OFF

SET OUTPUT_REDIRECT=^>nul
SET VERBOSE_PROMPT=Re-run the command with /v switch to see verbose output.
echo %OUTPUT%
FOR %%A IN (%*) DO (
	IF "%%A"=="/v" (
		SET "OUTPUT_REDIRECT="
		SET "VERBOSE_PROMPT="
	)
)

SET MSBUILD_PATH=D:\Program Files (x86)\MSBuild\14.0\Bin
SET NUGET_PATH=D:\Program Files (x86)\SiteExtensions\Kudu\59.51202.2583\bin\Scripts
IF NOT EXIST "%MSBUILD_PATH%" GOTO :not_app_service
IF NOT EXIST "%NUGET_PATH%" GOTO :not_app_service
ECHO Looks like this is an App Service environment. Updating PATH appropriately.
SET PATH=%cd%;%NUGET_PATH%;%MSBUILD_PATH%;%PATH%

:not_app_service
SET SAW_ROOT=%~dp0..\..
SET SOURCE_PATH=%SAW_ROOT%\.saw\src

CALL :verify_command nuget || exit /b 1
CALL :verify_command msbuild || exit /b 1

PUSHD %SOURCE_PATH%
ECHO Running NuGet restore...
nuget restore %OUTPUT_REDIRECT%
IF NOT %errorlevel%==0 (
	ECHO ERROR: NuGet restore failed. %VERBOSE_PROMPT%
	EXIT /B 1
)
ECHO Done!
POPD

ECHO Building SAW tools from the source code...
msbuild %SOURCE_PATH%\SolutionAuthoringWorkspace.sln /p:Configuration=Release %OUTPUT_REDIRECT%
IF NOT %errorlevel%==0 (
	ECHO ERROR: Unable to build SAW. %VERBOSE_PROMPT%
	EXIT /B 1
)
ECHO Done!

EXIT /b 0
GOTO :eof

:verify_command
where "%~1" >nul 2>nul
IF %errorlevel%==1 (
	ECHO ERROR: %~1 could not be found.
	EXIT /B 1
)
GOTO :eof
