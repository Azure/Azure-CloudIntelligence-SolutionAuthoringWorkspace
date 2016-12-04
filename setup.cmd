@ECHO OFF
rem
rem               Welcome to
rem                                         
rem   ____    _____     ____       _____  
rem   / ___)  (_   _)   / __ \     / ____\ 
rem  / /        | |    / /  \ \   ( (___   
rem ( (         | |   ( (    ) )   \___ \  
rem ( (         | |   ( (  /\) )       ) ) 
rem  \ \___    _| |__  \ \_\ \/    ___/ /  
rem   \____)  /_____(   \___\ \_  /____/   
rem                          \__)           
rem 
rem      Solution Authoring Workbench (SAW)
rem
rem
rem  >> DIRECTIONS
rem  
rem  If you haven't done so already, run this program to setup
rem  a new solution authoring environment. In VS Code Web (App
rem  Service Editor) that is as easy as clicking the ▶ `Run from
rem  Console` button in the top right corner.
rem
rem  Enjoy!
rem

SET MSBUILD_PATH=D:\Program Files (x86)\MSBuild\14.0\Bin
SET NUGET_PATH=D:\Program Files (x86)\SiteExtensions\Kudu\59.51202.2583\bin\Scripts
SET PATH=%cd%;%NUGET_PATH%;%MSBUILD_PATH%;%PATH%

CALL :verify_command git || exit /b 1
CALL :verify_command nuget || exit /b 1
CALL :verify_command msbuild || exit /b 1

PUSHD Source
ECHO Running NuGet restore...
nuget restore >nul
ECHO Done!
POPD

ECHO Building SAW tools from the source code...
msbuild Source/SolutionAuthoringWorkbench.sln /p:Configuration=Release >nul
ECHO Done!

:verify_command
where "%~1" >nul 2>nul
IF %errorlevel%==1 (
	ECHO ERROR: %~1 could not be found.
	EXIT /B 1
)
