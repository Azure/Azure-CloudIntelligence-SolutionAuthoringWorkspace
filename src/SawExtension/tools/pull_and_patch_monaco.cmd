SET MONACO_PATH=D:\Program Files (x86)\SiteExtensions\Monaco\1.0.0-20170123

IF EXIST monaco_original RMDIR /S /Q monaco_original
IF EXIST monaco_hacked RMDIR /S /Q monaco_hacked

xcopy /E "%MONACO_PATH%" monaco_original\

xcopy /E "%MONACO_PATH%" monaco_hacked\

patch -s -p0 < monaco_hacks.patch