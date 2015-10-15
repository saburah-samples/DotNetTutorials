using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Clients;
using Northwind.Services;
using System.Diagnostics;
using System.ServiceModel;
using System.Linq;
using System;

namespace Northwind.Tests
{
	[TestClass]
	public class OrderServiceTests
	{
		private static ServiceHost host;
		private OrderServiceClient client = new OrderServiceClient();

		[ClassInitialize]
		public static void StartSvcHost(TestContext context)
		{
			host = new ServiceHost(typeof(OrderService));
			host.Open();
			Trace.WriteLine("Service started.");
		}

		[ClassCleanup]
		public static void StopSvcHost()
		{
			host.Close();
			Trace.WriteLine("Service stoped.");
		}

		[TestMethod]
		public void TestGetOrders()
		{
			var orders = client.GetOrders();
			Assert.IsNotNull(orders);
			var order = orders.FirstOrDefault();
			if (order != null)
			{
				Assert.IsTrue(order.OrderID > 0, "OrderID is invalid");
				Assert.IsTrue(order.OrderDetails != null, "OrderDetails is null");
				Assert.IsTrue(!order.OrderDetails.Any(), "OrderDetails is not empty");
			}
			else
			{
				Trace.WriteLine("no orders found");
			}
		}

		[TestMethod]
		public void TestGetOrder()
		{
			Trace.WriteLine("get order if not exists:");
			try
			{
				var actual = client.GetOrder(-1);
				Assert.IsNull(actual);
			}
			catch (FaultException ex)
			{
				Assert.IsNotNull(ex);
			}

			Trace.WriteLine("get order if exists:");
			var orders = client.GetOrders();
			Assert.IsNotNull(orders);
			var order = orders.FirstOrDefault();
			if (order != null)
			{
				var actual = client.GetOrder(order.OrderID);
				Assert.AreEqual(order.OrderID, actual.OrderID, "OrderID is invalid");
				Assert.IsTrue(actual.OrderDetails != null, "OrderDetails is null");
				Assert.IsTrue(actual.OrderDetails.Any(), "OrderDetails is empty");
			}
			else
			{
				Trace.WriteLine("no orders found");
			}
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
