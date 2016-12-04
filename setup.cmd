@ECHO OFF

SET MSBUILD_PATH=D:\Program Files (x86)\MSBuild\14.0\Bin
SET NUGET_PATH=D:\Program Files (x86)\SiteExtensions\Kudu\59.51202.2583\bin\Scripts
SET PATH=%NUGET_PATH%;%MSBUILD_PATH%;%PATH%

where msbuild >nul 2>nul
IF %errorlevel%==1 (
	ECHO ERROR: MSBuild could not be found.
	EXIT /B 1
)

where nuget >nul 2>nul
IF %errorlevel%==1 (
	ECHO ERROR: NuGet could not be found.
	EXIT /B 1
)

pushd Source
nuget restore
popd

msbuild Source/SolutionAuthoringWorkbench.sln /p:Configuration=Release 
