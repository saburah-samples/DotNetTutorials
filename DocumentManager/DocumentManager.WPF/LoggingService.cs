using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentManager.WPF
{
    public class LoggingService
    {
        public void WriteTrace(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }

        public void WriteDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
