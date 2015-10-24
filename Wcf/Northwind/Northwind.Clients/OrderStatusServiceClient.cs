using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Northwind.Clients
{
	public class OrderStatusServiceClient : IOrderStatusService, IOrderStatusCallback, IDisposable
	{
		public event OrderStatusChangedEventHandler OrderStatusChanged;

		private ServiceAdapter<IOrderStatusService> adapter;

		public OrderStatusServiceClient()
		{
			var endpointConfigurationName = ConfigurationManager.AppSettings.Get(this.GetType().Name);
			this.adapter = new ServiceAdapter<IOrderStatusService>(endpointConfigurationName, new InstanceContext(this));
		}

		~OrderStatusServiceClient()
		{
			Dispose(false);
		}

		public void Subscribe()
		{
			adapter.Execute(s => s.Subscribe());
		}

		public void Unsubscribe()
		{
			adapter.Execute(s => s.Unsubscribe());
		}

		void IOrderStatusCallback.OnOrderStatusChanged(int orderId, OrderStatus status)
		{
			if (OrderStatusChanged != null)
			{
				OrderStatusChanged(orderId, status);
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				Unsubscribe();
				adapter.Close();
			}
		}
	}
}
