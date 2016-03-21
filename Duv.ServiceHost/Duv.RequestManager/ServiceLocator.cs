using Duv.RequestManager.Data;
using Duv.RequestManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.RequestManager
{
	public static class ServiceLocator
	{
		private static readonly Dictionary<Type, Func<object>> serviceTypes;

		static ServiceLocator()
		{
			serviceTypes = new Dictionary<Type, Func<object>>();
			serviceTypes.Add(typeof(IRequestQueueService), () => new RequestQueueService());
			//serviceTypes.Add(typeof(IRequestProcessService), () => new RequestProcessService());
			serviceTypes.Add(typeof(IRequestRepository), () => new RequestRepository());
		}

		public static T GetService<T>()
		{
			var type = typeof(T);
			if (serviceTypes.ContainsKey(type))
			{
				var factory = serviceTypes[type];
				var service = factory.Invoke();
				return (T)service;
			}

			throw new NotSupportedException(string.Format("Failed to resolve service type {0}", type.FullName));
		}
	}
}
