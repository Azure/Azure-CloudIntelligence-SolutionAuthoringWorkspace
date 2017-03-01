SET BASE=%HOME%\saw\workspace
SET MONACO_PATH=D:\Program Files (x86)\SiteExtensions\Monaco\1.0.0-20170123
SET MONACO_HACKED_PATH=D:\home\SiteExtensions\SawExtension\monaco_hacked\
SET GIT_PATH=D:\Program Files (x86)\Git\bin

SET PATH=%GIT_PATH%;%PATH%

IF EXIST "%MONACO_HACKED_PATH%" RMDIR /S /Q "%MONACO_HACKED_PATH%"
xcopy /E /Q "%MONACO_PATH%" "%MONACO_HACKED_PATH%"

patch -s -p0 < monaco_hacks.patch

IF NOT EXIST "%BASE%\Solutions" MKDIR "%BASE%\Solutions"

pushd %BASE%
git init
git remote add origin https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace
git fetch --depth=1 origin
git checkout origin/master -- Samples
rmdir /S /Q .git
popd

bin\saw configure -SolutionsDirectory "%BASE%\Solutions" -SolutionStorageConnectionString "%CUSTOMCONNSTR_SolutionStorageConnectionString%"
bin\saw deploy
