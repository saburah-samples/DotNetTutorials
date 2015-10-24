using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Northwind.Clients
{
	public class OrderStatusServiceClient : IOrderStatusService, IOrderStatusCallback
	{
		public event OrderStatusChangedEventHandler OrderStatusChanged;

		private ServiceAdapter<IOrderStatusService> adapter;

		public OrderStatusServiceClient()
		{
			var endpointConfigurationName = ConfigurationManager.AppSettings.Get(this.GetType().Name);
			this.adapter = new ServiceAdapter<IOrderStatusService>(endpointConfigurationName, CreateCallbackInstance);
		}

		private InstanceContext CreateCallbackInstance()
		{
			return new InstanceContext(this);
		}

		public void Open()
		{
			adapter.Open();
		}

		public void Close()
		{
			adapter.Close();
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
				try
				{
					OrderStatusChanged(orderId, status);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex, "Invalid callback handler");
					throw ex;
				}
			}
		}
	}
}
