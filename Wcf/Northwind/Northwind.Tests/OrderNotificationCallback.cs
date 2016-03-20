using System;
using Northwind.Contracts;
using System.Collections.Generic;
using System.Diagnostics;

namespace Northwind.Tests
{
	internal class OrderNotificationCallback : IOrderStatusCallback
	{
		private List<string> log = new List<string>();

		public void OnOrderStatusChanged(int orderId, OrderStatus status)
		{
			var message = string.Format("{0} - {1}", orderId, status);
			log.Add(message);
			Trace.WriteLine(message);
		}

		public IEnumerable<string> Log { get { return log; } }
	}
}