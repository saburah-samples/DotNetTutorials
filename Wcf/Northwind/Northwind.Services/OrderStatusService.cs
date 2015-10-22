using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Timers;

namespace Northwind.Services
{
	public class OrderStatusService : ServiceBase, IOrderStatusService
	{
		private List<IOrderStatusCallback> subscribers = new List<IOrderStatusCallback>();

		public void Subscribe()
		{
			var subscriber = OperationContext.Current.GetCallbackChannel<IOrderStatusCallback>();
			if (!IsSubscriberRegistered(subscriber))
			{
				RegisterSubscriber(subscriber);
			}

			var timer = new Timer(1000);
			timer.Elapsed += Timer_Elapsed;
			timer.Enabled = true;
		}

		public void Unsubscribe()
		{
			var subscriber = OperationContext.Current.GetCallbackChannel<IOrderStatusCallback>();
			if (IsSubscriberRegistered(subscriber))
			{
				UnregisterSubscriber(subscriber);
			}
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			subscribers.ForEach(s => s.OnOrderStatusChanged(1, OrderStatus.Draft));
		}

		private bool IsSubscriberRegistered(IOrderStatusCallback subscriber)
		{
			return subscribers.Contains(subscriber);
		}

		private void RegisterSubscriber(IOrderStatusCallback subscriber)
		{
			subscribers.Add(subscriber);
		}

		private void UnregisterSubscriber(IOrderStatusCallback subscriber)
		{
			subscribers.Remove(subscriber);
		}
	}
}
