set buildPath=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set buildExe="%buildPath%\MSBuild.exe"

set BUILD_NUMBER=1.0.0.3
rem call %buildExe% %~1.build /t:Clean
call %buildExe% %~1.build /t:Publish
