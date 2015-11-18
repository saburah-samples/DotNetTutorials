using Northwind.Contracts;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace Northwind.Clients
{
	public class ServiceAdapter<TService> where TService : class, IContract
	{
		private ICommunicationObject proxy;
		private ChannelFactory<TService> channelFactory;

		public ServiceAdapter(string endpointConfigurationName)
		{
			this.channelFactory = new ChannelFactory<TService>(endpointConfigurationName);
		}

		public ServiceAdapter(string endpointConfigurationName, Func<InstanceContext> callbackInstanceFactory)
		{
			var callbackInstance = callbackInstanceFactory.Invoke();
			this.channelFactory = new DuplexChannelFactory<TService>(callbackInstance, endpointConfigurationName);
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
				HandleError(ex);
				throw ex;
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
				HandleError(ex);
				throw ex;
			}
		}

		private void HandleError(Exception ex)
		{
			Trace.WriteLine(ex, this.GetType().FullName);
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
		}

		private TService GetService()
		{
			return (TService)GetProxy();
		}

		private ICommunicationObject GetProxy()
		{
			if (this.proxy == null)
			{
				this.proxy = (ICommunicationObject)channelFactory.CreateChannel();
            }
			else if (proxy.State != CommunicationState.Created && proxy.State != CommunicationState.Opened)
			{
				proxy.Abort();
				this.proxy = (ICommunicationObject)channelFactory.CreateChannel();
			}

			Trace.WriteLine(proxy.State, "CommunicationState");
			return this.proxy;
		}
	}
}
