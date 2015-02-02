@echo off
set SOLUTION_NAME=PowerCollections

set BUILD_PATH=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set BUILD="%BUILD_PATH%\MSBuild.exe"
rem %BUILDER% /? >MSBuild.hlp

echo Build solution "%SOLUTION_NAME%" ...
call %BUILD% Source\%SOLUTION_NAME%.sln /t:Rebuild /p:Configuration=Release >%~n0.log
echo Done.
