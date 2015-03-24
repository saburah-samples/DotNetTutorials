set buildPath=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set buildExe="%buildPath%\MSBuild.exe"

call %buildExe% %~1.targets /p:Configuration=Release
