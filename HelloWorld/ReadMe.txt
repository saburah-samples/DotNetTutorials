�������:
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\MakeCert.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\CertMgr.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\Mage.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\MakeCert.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\CertMgr.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\pvk2pfx.exe

�������� �����������
1. ������������� ���������� HelloWpf � ��������� ������������ My
	makecert.exe -r -pe -a sha1 -n "CN=HelloWpf" -ss My
2. ������� ���������� HelloWpf �� ��������� My ��� ���������� ���������� Project-Properties-Signing
3. ��������� ���������� � "���������� �������� ������ ������������" - ����� UAC �� ������� ���������
	certmgr.exe -add certificate.cer -c -s -r localMachine TrustedPublisher

������������� ClickOnce ��� �������� �������������� ����������
https://www.techdays.ru/videos/1274.html

Creating Publisher Certificates For ClickOnce
http://geekswithblogs.net/kobush/archive/2005/05/30/41068.aspx
