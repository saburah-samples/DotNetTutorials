�������:
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\MakeCert.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\CertMgr.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\Mage.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\MakeCert.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\CertMgr.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\pvk2pfx.exe
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe

�������� �����������
1. ������������� ���������� HelloWpf � ��������� ������������ My
	makecert.exe -r -pe -a sha1 -n "CN=HelloWpf" -ss My
2. ������� ���������� HelloWpf �� ��������� My ��� ���������� ���������� Project-Properties-Signing
3. ��������� ���������� � "���������� �������� ������ ������������" - ����� UAC �� ������� ���������
	certmgr.exe -add certificate.cer -c -s -r localMachine TrustedPublisher

System.Deployment.Application Namespace
https://msdn.microsoft.com/en-us/library/system.deployment.application(v=vs.110).aspx

������������� ClickOnce ��� �������� �������������� ����������
https://www.techdays.ru/videos/1274.html
00:13:00 - ������ ������������� ApplicationDeployment
00:15:00 - �������� ���������� � �������� �������������
00:16:00 - ������������� �� ��������� ������ msbuild /t:publish

Creating Publisher Certificates For ClickOnce
http://geekswithblogs.net/kobush/archive/2005/05/30/41068.aspx

MSBuild
https://msdn.microsoft.com/ru-ru/library/dd393574.aspx
�������� ����������� MSBuild
https://msdn.microsoft.com/ru-ru/library/dd637714.aspx
��������� �����������. �������� ����� ������� MSBuild � ����
https://msdn.microsoft.com/ru-ru/library/dd576348.aspx
���������� �������� � ������� MSBuild
https://msdn.microsoft.com/ru-ru/library/7z253716.aspx
������� Output (MSBuild)
https://msdn.microsoft.com/ru-ru/library/ms164287.aspx
���������� �������� � ����� ������ �������� MSBuild
https://msdn.microsoft.com/ru-ru/library/5dy88c2e.aspx