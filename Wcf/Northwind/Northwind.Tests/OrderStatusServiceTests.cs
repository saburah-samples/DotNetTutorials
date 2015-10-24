using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Clients;
using Northwind.Contracts;
using Northwind.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace Northwind.Tests
{
	[TestClass]
	public class OrderStatusServiceTests
	{
		private static ServiceHost host;
		private List<string> log;

		[ClassInitialize]
		public static void StartSvcHost(TestContext context)
		{
			host = new ServiceHost(typeof(OrderStatusService));
			host.Open();
			Trace.WriteLine("Service started.");
		}

		[ClassCleanup]
		public static void StopSvcHost()
		{
			host.Close();
			Trace.WriteLine("Service stoped.");
		}

		public OrderStatusServiceTests()
		{
			log = new List<string>();
		}

		[TestMethod]
		public void Test_RepeatedSubscribeUnsubscribe()
		{
			var client = new OrderStatusServiceClient();
			client.Unsubscribe();
			client.Subscribe();
			client.Subscribe();
			client.Unsubscribe();
		}

		[TestMethod]
		public void Test_InactiveSubscriber()
		{
			var client = new OrderStatusServiceClient();
			client.OrderStatusChanged += client_OrderStatusChanged;
			client.Subscribe();
			log.Clear();
			client.Close();
			Thread.Sleep(5 * 1000);
			Assert.IsFalse(log.Any(), "event log should be empty.");
		}

		[TestMethod]
		public void Test_SubscribeUnsubscribe()
		{
			var client = new OrderStatusServiceClient();
			client.OrderStatusChanged += client_OrderStatusChanged;

			client.Subscribe();
			log.Clear();
			Thread.Sleep(5 * 1000);
			Assert.IsTrue(log.Any(), "event log should be filled.");

			client.Unsubscribe();
			log.Clear();
			Thread.Sleep(5 * 1000);
			Assert.IsFalse(log.Any(), "event log should be empty.");
		}

		[TestMethod]
		public void Test_FailedCallbackHandler()
		{
			var client = new OrderStatusServiceClient();
			client.OrderStatusChanged += client_FailedOrderStatusChanged;
			client.Subscribe();
			Thread.Sleep(5 * 1000);
			client.Unsubscribe();
		}

		void client_OrderStatusChanged(int orderId, OrderStatus status)
		{
			var message = string.Format("{0} - {1}", orderId, status);
			Trace.WriteLine(message);
			log.Add(message);
		}

		void client_FailedOrderStatusChanged(int orderId, OrderStatus status)
		{
			Assert.IsTrue(false);
		}
	}
}
