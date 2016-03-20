using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Services;
using System.ServiceModel;
using System.Diagnostics;
using Northwind.Clients;
using System.Threading;
using Northwind.Contracts;
using System.Linq;

namespace Northwind.Tests
{
	[TestClass]
	public class OrderNotificationServiceTests
	{
		private static ServiceHost host;

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

		[TestMethod]
		public void TestSubscribeOnNotification()
		{
			var callback = new OrderNotificationCallback();
			var client = new OrderNotificationClient(callback);

			Thread.Sleep(5 * 1000);
			Assert.IsTrue(callback.Log.Any());
		}
	}
}
