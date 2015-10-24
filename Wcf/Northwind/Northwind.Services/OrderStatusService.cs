using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Timers;

namespace Northwind.Services
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class OrderStatusService : ServiceBase, IOrderStatusService
	{
		private List<IOrderStatusCallback> subscribers = new List<IOrderStatusCallback>();
		private Timer timer;

		public OrderStatusService()
		{
			timer = new Timer(1000);
			timer.Elapsed += (s, e) => NotifySubscribers();
		}

		~OrderStatusService()
		{
			timer.Dispose();
		}

		public void Subscribe()
		{
			var subscriber = OperationContext.Current.GetCallbackChannel<IOrderStatusCallback>();
			if (!IsSubscriberRegistered(subscriber))
			{
				RegisterSubscriber(subscriber);
			}

			timer.Enabled = subscribers.Any();
		}

		public void Unsubscribe()
		{
			var subscriber = OperationContext.Current.GetCallbackChannel<IOrderStatusCallback>();
			if (IsSubscriberRegistered(subscriber))
			{
				UnregisterSubscriber(subscriber);
			}

			timer.Enabled = subscribers.Any();
		}

		private void NotifySubscribers()
		{
			var inactiveSubscribers = new List<IOrderStatusCallback>();
			var activeSubscribers = subscribers.ToArray();
			foreach (var s in activeSubscribers)
			{
				try
				{
					s.OnOrderStatusChanged(1, OrderStatus.Draft);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex, "Inactive subscriber");
					inactiveSubscribers.Add(s);
				}
			}
			foreach (var s in inactiveSubscribers)
			{
				UnregisterSubscriber(s);
			}
		}

		private bool IsSubscriberRegistered(IOrderStatusCallback subscriber)
		{
			return subscribers.Contains(subscriber);
		}

		private void RegisterSubscriber(IOrderStatusCallback subscriber)
		{
			subscribers.Add(subscriber);
			Trace.WriteLine("Subscriber is added.");
		}

		private void UnregisterSubscriber(IOrderStatusCallback subscriber)
		{
			subscribers.Remove(subscriber);
			Trace.WriteLine("Subscriber is removed.");
		}
	}
}
