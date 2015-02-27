using DocumentManager.WPF.PersonManagerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DocumentManager.WPF
{
    public class AppServices
    {
        public static void Startup()
        {
            Instance = new AppServices();
            Instance.Logger = new LoggingService();
            Instance.Persistence = new PersistenceService();
            Instance.MainWindow = new MainWindow();
            Instance.PersonManager = new PersonManager();
            Instance.Run();
        }

        public static void Resume()
        {
            WriteLog("app services resume");
        }

        public static void Suspend()
        {
            WriteLog("app services suspend");
        }

        public static void Shutdown()
        {
            WriteLog("app services shutdown");
        }

        public static void WriteLog(string message)
        {
            if (Instance != null && Instance.Logger != null)
                Instance.Logger.WriteDebug(message);
        }

        public static AppServices Instance { get; private set; }

        private void Run()
        {
            WriteLog("app services startup");
            MainWindow.ContentPane.Content = PersonManager;
            MainWindow.Show();
        }

        protected MainWindow MainWindow { get; private set; }

        protected LoggingService Logger { get; private set; }

        protected PersistenceService Persistence { get; private set; }

        protected PersonManager PersonManager { get; private set; }
    }
}
