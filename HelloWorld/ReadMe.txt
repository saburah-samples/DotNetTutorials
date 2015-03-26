Утилиты:
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\MakeCert.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\CertMgr.exe
C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin\Mage.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\MakeCert.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\CertMgr.exe
C:\Program Files (x86)\Windows Kits\8.1\bin\x64\pvk2pfx.exe
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe

Создание сертификата
1. Сгенерировать сертификат HelloWpf в хранилище сертификатов My
	makecert.exe -r -pe -a sha1 -n "CN=HelloWpf" -ss My
2. Выбрать сертификат HelloWpf из хранилища My для подписания приложения Project-Properties-Signing
3. Поместить сертификат в "Доверенные корневые центры сертификации" - чтобы UAC не выдавал сообщения
	certmgr.exe -add certificate.cer -c -s -r localMachine TrustedPublisher

System.Deployment.Application Namespace
https://msdn.microsoft.com/en-us/library/system.deployment.application(v=vs.110).aspx

Использование ClickOnce для быстрого разворачивания приложений
https://www.techdays.ru/videos/1274.html
00:13:00 - пример использования ApplicationDeployment
00:15:00 - манифест приложения и манифест развертывания
00:16:00 - развертывание из командной строки msbuild /t:publish

Creating Publisher Certificates For ClickOnce
http://geekswithblogs.net/kobush/archive/2005/05/30/41068.aspx

MSBuild
https://msdn.microsoft.com/ru-ru/library/dd393574.aspx
Основные возможности MSBuild
https://msdn.microsoft.com/ru-ru/library/dd637714.aspx
Пошаговое руководство. Создание файла проекта MSBuild с нуля
https://msdn.microsoft.com/ru-ru/library/dd576348.aspx
Справочные сведения о задачах MSBuild
https://msdn.microsoft.com/ru-ru/library/7z253716.aspx
Элемент Output (MSBuild)
https://msdn.microsoft.com/ru-ru/library/ms164287.aspx
Справочные сведения о схеме файлов проектов MSBuild
https://msdn.microsoft.com/ru-ru/library/5dy88c2e.aspx