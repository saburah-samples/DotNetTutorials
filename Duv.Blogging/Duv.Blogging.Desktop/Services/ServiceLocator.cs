using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.Services
{
    class ServiceLocator
    {
        static ServiceLocator()
        {
            BloggingService = new MockupBloggingService();
        }

        public static IBloggingService BloggingService { get; private set; }
    }
}
