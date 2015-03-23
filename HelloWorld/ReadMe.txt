Утилиты:
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\MakeCert.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\CertMgr.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\Mage.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\MakeCert.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\CertMgr.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\pvk2pfx.exe

Создание сертификата
1. Сгенерировать сертификат HelloWpf в хранилище сертификатов My
	makecert.exe -r -pe -a sha1 -n "CN=HelloWpf" -ss My
2. Выбрать сертификат HelloWpf из хранилища My для подписания приложения Project-Properties-Signing
3. Поместить сертификат в "Доверенные корневые центры сертификации" - чтобы UAC не выдавал сообщения
	certmgr.exe -add certificate.cer -c -s -r localMachine TrustedPublisher

Использование ClickOnce для быстрого разворачивания приложений
https://www.techdays.ru/videos/1274.html

Creating Publisher Certificates For ClickOnce
http://geekswithblogs.net/kobush/archive/2005/05/30/41068.aspx
