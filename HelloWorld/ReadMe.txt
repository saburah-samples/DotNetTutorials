Creating Publisher Certificates For ClickOnce
http://geekswithblogs.net/kobush/archive/2005/05/30/41068.aspx
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\MakeCert.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\CertMgr.exe
Сгенерировать сертификат HelloWpf
	makecert.exe -r -pe -a sha1 -n "CN=HelloWpf" -b 01/01/2015 -e 01/01/2036 -eku 1.3.6.1.5.5.7.3.3 -ss My HelloWpf.cer
Поместить сертификат в "Доверенные корневые центры сертификации"
Выбрать сертификат HelloWpf для подписания приложения Project-Properties-Signing
