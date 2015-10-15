using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Clients;
using Northwind.Services;
using System.ServiceModel;
using System.Diagnostics;

namespace Northwind.Tests
{
	[TestClass]
	public class OrderServiceTests
	{
		private OrderServiceClient client = new OrderServiceClient();
		private ServiceHost host;

		[TestInitialize]
		public void StartSvcHost()
		{
			this.host = new ServiceHost(typeof(OrderService));
			this.host.Open();
			Trace.WriteLine("Service started.");
		}

		[TestCleanup]
		public void StopSvcHost()
		{
			this.host.Close();
			Trace.WriteLine("Service stoped.");
		}

		[TestMethod]
		public void TestGetOrders()
		{
			var orders = client.GetOrders();
		}

		[TestMethod]
		public void TestGetOrderById()
		{
			var orderId = 1;
			var order = client.GetOrder(orderId);
		}

		[TestMethod]
		public void TestCreateOrder()
		{
			var order1 = new Contracts.Order();
			var order2 = client.CreateOrder(order1);
		}

		[TestMethod]
		public void TestUpdateOrder()
		{
			var order1 = new Contracts.Order();
			var order2 = client.UpdateOrder(order1);
		}

		[TestMethod]
		public void TestDeleteOrder()
		{
			var orderId = 1;
			client.DeleteOrder(orderId);
		}

		[TestMethod]
		public void TestApproveOrder()
		{
			var orderId = 1;
			var order = client.ApproveOrder(orderId);
		}

		[TestMethod]
		public void TestCompleteOrder()
		{
			var orderId = 1;
			var order = client.CompleteOrder(orderId);
		}
	}
}
