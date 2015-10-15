using Northwind.Contracts;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Northwind.Clients
{
	public class ServiceAdapter<TService> where TService : class, IContract
	{
		private WcfProxy<TService> proxy;
		private string endpointConfigurationName;

		public ServiceAdapter(string endpointConfigurationName)
		{
			this.endpointConfigurationName = endpointConfigurationName;
		}

		public void Execute(Action<TService> command)
		{
			try
			{
				var service = GetService();
				command.Invoke(service);
			}
			catch (Exception ex)
			{
				if (!HandleError(ex))
				{
					throw ex;
				}
			}
		}

		public TResult Execute<TResult>(Func<TService, TResult> command)
		{
			try
			{
				var service = GetService();
				return command.Invoke(service);
			}
			catch (Exception ex)
			{
				if (!HandleError(ex))
				{
					throw ex;
				}
				else
				{
					return default(TResult);
				}
			}
		}

		private bool HandleError(Exception ex)
		{
			Trace.WriteLine(ex);
			var handled = false;
			if (ex is FaultException)
			{
				if (proxy.State == CommunicationState.Faulted)
				{
					proxy = null;
				}
			}
			else if (ex is CommunicationException)
			{
				proxy = null;
			}
			return handled;
		}

		private TService GetService()
		{
			return GetProxy().WcfChannel;
		}

		private WcfProxy<TService> GetProxy()
		{
			if (this.proxy == null)
			{
				this.proxy = new WcfProxy<TService>(this.endpointConfigurationName);
			}

			return this.proxy;
		}

		private class WcfProxy<T> : ClientBase<T> where T : class, IContract
		{
			public WcfProxy(string endpointConfigurationName)
				: base(endpointConfigurationName) { }

			public T WcfChannel
			{
				get { return base.Channel; }
			}
		}
	}
}
