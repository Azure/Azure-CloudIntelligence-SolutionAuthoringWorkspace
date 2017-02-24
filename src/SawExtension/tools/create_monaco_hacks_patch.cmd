SET MONACO_ORIGINAL_PATH=D:\Program Files (x86)\SiteExtensions\Monaco\1.0.0-20170123
SET MONACO_HACKED_PATH=D:\home\SiteExtensions\SawExtension\monaco_hacked

IF EXIST monaco_original RMDIR /S /Q monaco_original
IF EXIST monaco_hacked RMDIR /S /Q monaco_hacked

xcopy /E "%MONACO_ORIGINAL_PATH%" monaco_original\
xcopy /E "%MONACO_HACKED_PATH%" monaco_hacked\

diff -ruN --ignore-all-space monaco_original/ monaco_hacked/ > monaco_hacks.patch

RMDIR /S /Q monaco_original
RMDIR /S /Q monaco_hacked
