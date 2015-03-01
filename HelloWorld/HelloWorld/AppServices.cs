using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class AppServices
    {
        public AppServices()
        {
            
        }

        public void Startup(App app, string[] args)
        {
            WriteDebug("app services are starting up...");
            App = app;
            if (MainWindow == null)
                WriteTrace("MainWindow = null");
        }

        public void Resume(EventArgs e)
        {
            WriteDebug("app services are resuming...");
            if (MainWindow != null)
                WriteTrace("MainWindow != null");
        }

        public void Suspend(EventArgs e)
        {
            WriteDebug("app services are suspending...");
            if (MainWindow != null)
                WriteTrace("MainWindow != null");
        }

        public void Shutdown(int exitCode)
        {
            WriteDebug("app services are shutting down...");
            if (MainWindow == null)
                WriteTrace("MainWindow == null");
            App = null;
        }

        private void WriteDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        private void WriteTrace(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }

        public App App { get; private set; }

        public MainWindow MainWindow
        {
            get
            {
                if (App != null && App.MainWindow != null)
                    return App.MainWindow as MainWindow;
                else
                    return null;
            }
        }
    }
}
