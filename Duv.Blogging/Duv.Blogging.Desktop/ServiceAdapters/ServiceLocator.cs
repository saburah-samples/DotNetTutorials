using Duv.Blogging.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.ServiceAdapters
{
    class ServiceLocator
    {
        private const string ERROR_CONTRACT_IMPLEMENTATION_UNDEFINED = "Implementation of contract '{0}' was not defined";

        private static readonly Dictionary<Type, Type> serviceMap;

        static ServiceLocator()
        {
            serviceMap = new Dictionary<Type, Type>();
            serviceMap.Add(typeof(IBloggingService), typeof(MockupBloggingService));
        }

        public static TService GetService<TService>()
        {
            var contractType = typeof(TService);
            if (serviceMap.ContainsKey(contractType))
                return (TService)Activator.CreateInstance(serviceMap[contractType]);
            else
                throw new NotImplementedException(string.Format(ERROR_CONTRACT_IMPLEMENTATION_UNDEFINED, contractType.Name));
        }
    }
}
