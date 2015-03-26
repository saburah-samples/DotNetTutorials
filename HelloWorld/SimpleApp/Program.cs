using System;
using System.Collections.Generic;
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
                Console.WriteLine("ClickOnce Enabled");
            else
                Console.WriteLine("ClickOnce Disabled");
            Console.WriteLine("BaseDirectory={0}", AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
