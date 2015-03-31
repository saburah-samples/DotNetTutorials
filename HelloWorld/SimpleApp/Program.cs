using HelloSettings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                // если приложение было установлено
                Console.WriteLine("ClickOnce Enabled");
                var deployment = ApplicationDeployment.CurrentDeployment;
                Console.WriteLine("Deployment version: {0}", deployment.CurrentVersion);
            }
            else
            {
                // если приложение не было установлено
                Console.WriteLine("ClickOnce Disabled");
            }
            Console.WriteLine("BaseDirectory={0}", AppDomain.CurrentDomain.BaseDirectory);

            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = (StartupFoldersConfigSection)cfg.Sections["StartupFolders"];
            if (section != null)
            {
                //TODO: foreach (var item in section.FolderItems)
                for (var i = 0; i < section.FolderItems.Count;i++ )
                {
                    var item = section.FolderItems[i];
                    Console.WriteLine("{0}={1}", item.FolderType, item.Path);
                }
                section.FolderItems[0].Path = @"C:\Nanook";
                cfg.Save(); //устанавливает перенос на новую строку и производит проверку <exename>.vshost.exe.config файла в вашей отладочной папке.
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
