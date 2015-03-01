using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AppServices appServices = new AppServices();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            appServices.Startup(this, e.Args);
        }
        
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            appServices.Resume(e);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            appServices.Suspend(e);
            base.OnDeactivated(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            appServices.Shutdown(e.ApplicationExitCode);
            base.OnExit(e);
        }
    }
}
