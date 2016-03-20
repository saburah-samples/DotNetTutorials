using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Northwind.Clients
{
	public class OrderNotificationClient : IOrderStatusService
	{
		private readonly ServiceAdapter<IOrderStatusService> adapter;

		public OrderNotificationClient(IOrderStatusCallback callback)
		{
			var endpointConfigurationName = ConfigurationManager.AppSettings.Get(this.GetType().Name);
			this.adapter = new ServiceAdapter<IOrderStatusService>(endpointConfigurationName, new InstanceContext(callback));
		}

		public void Subscribe()
		{
			adapter.Execute(s => s.Subscribe());
		}

		public void Unsubscribe()
		{
			adapter.Execute(s => s.Unsubscribe());
		}
	}
}
