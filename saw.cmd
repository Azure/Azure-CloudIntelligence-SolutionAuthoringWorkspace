@ECHO OFF

CALL %~dp0\.saw\scripts\env.cmd >nul

"%SAW_ROOT%\.saw\bin\SawCli.exe" %*
