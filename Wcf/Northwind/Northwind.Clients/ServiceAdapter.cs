using Northwind.Contracts;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Northwind.Clients
{
	public class ServiceAdapter<TService> where TService : class, IContract
	{
		private Func<IWcfProxy<TService>> proxyFactory;
		private Func<InstanceContext> callbackFactory;

		private IWcfProxy<TService> proxy;
		private string endpointConfigurationName;

		public ServiceAdapter(string endpointConfigurationName)
		{
			this.endpointConfigurationName = endpointConfigurationName;
			this.proxyFactory = CreateProxy;
		}

		public ServiceAdapter(string endpointConfigurationName, Func<InstanceContext> callbackInstanceFactory)
		{
			this.endpointConfigurationName = endpointConfigurationName;
			this.callbackFactory = callbackInstanceFactory;
			this.proxyFactory = CreateDuplexProxy;
		}

		private IWcfProxy<TService> CreateProxy()
		{
			return new WcfProxy<TService>(endpointConfigurationName);
		}

		private IWcfProxy<TService> CreateDuplexProxy()
		{
			var callbackInstance = callbackFactory.Invoke();
			return new WcfDuplexProxy<TService>(endpointConfigurationName, callbackInstance);
		}

		public void Open()
		{
			proxy = null;
			GetProxy().Open();
		}

		public void Close()
		{
			if (proxy != null)
			{
				try
				{
					proxy.Close();
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex, this.GetType().FullName);
					proxy.Abort();
				}
				finally
				{
					proxy = null;
				}
			}
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
			Trace.WriteLine(ex, this.GetType().FullName);
			var handled = false;
			if (ex is FaultException)
			{
				if (proxy.State == CommunicationState.Faulted)
				{
					proxy.Abort();
					proxy = null;
				}
			}
			else if (ex is CommunicationException)
			{
				proxy.Abort();
				proxy = null;
			}
			return handled;
		}

		private TService GetService()
		{
			return GetProxy().WcfChannel;
		}

		private IWcfProxy<TService> GetProxy()
		{
			if (this.proxy == null)
			{
				this.proxy = proxyFactory.Invoke();
			}
			else if (proxy.State != CommunicationState.Created && proxy.State != CommunicationState.Opened)
			{
				proxy.Abort();
				this.proxy = proxyFactory.Invoke();
			}

			Trace.WriteLine(proxy.State, "CommunicationState");
			return this.proxy;
		}

		private interface IWcfProxy<T> : ICommunicationObject where T : class, IContract
		{
			T WcfChannel { get; }
		}

		private class WcfProxy<T> : ClientBase<T>, IWcfProxy<T> where T : class, IContract
		{
			public WcfProxy(string endpointConfigurationName)
				: base(endpointConfigurationName) { }

			public T WcfChannel
			{
				get { return base.Channel; }
			}
		}

		private class WcfDuplexProxy<T> : DuplexClientBase<T>, IWcfProxy<T> where T : class, IContract
		{
			public WcfDuplexProxy(string endpointConfigurationName, InstanceContext callbackInstance)
				: base(callbackInstance, endpointConfigurationName) { }

			public T WcfChannel
			{
				get { return base.Channel; }
			}
		}
	}
}
