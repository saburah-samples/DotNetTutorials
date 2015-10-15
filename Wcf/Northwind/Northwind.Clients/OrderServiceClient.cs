using Northwind.Contracts;
using System.Collections.Generic;
using System.Configuration;

namespace Northwind.Clients
{
	public class OrderServiceClient : IOrderService
	{
		protected readonly ServiceAdapter<IOrderService> adapter;

		public OrderServiceClient()
		{
			var endpointConfigurationName = ConfigurationManager.AppSettings.Get(this.GetType().Name);
			this.adapter = new ServiceAdapter<IOrderService>(endpointConfigurationName);
		}

		public IEnumerable<Order> GetOrders()
		{
			return adapter.Execute(s => s.GetOrders());
		}

		public Order GetOrder(int orderId)
		{
			return adapter.Execute(s => s.GetOrder(orderId));
		}

		public Order CreateOrder(Order order)
		{
			return adapter.Execute(s => s.CreateOrder(order));
		}

		public Order UpdateOrder(Order order)
		{
			return adapter.Execute(s => s.UpdateOrder(order));
		}

		public void DeleteOrder(int orderId)
		{
			adapter.Execute(s => s.DeleteOrder(orderId));
		}

		public Order ApproveOrder(int orderId)
		{
			return adapter.Execute(s => s.ApproveOrder(orderId));
		}

		public Order CompleteOrder(int orderId)
		{
			return adapter.Execute(s => s.CompleteOrder(orderId));
		}
	}
}
