using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;

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
                var handled = false;
                OnError(ex, out handled);
                if (!handled) throw ex;
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
                var handled = false;
                OnError(ex, out handled);
                if (!handled) throw ex;
                return default(TResult);
            }
        }

        protected void OnError(Exception ex, out bool handled)
        {
            handled = false;
            if (ex is FaultException)
            {
                Trace.WriteLine("Communication channel is faulted.");
                Trace.WriteLine((ex as FaultException).Code.Name + ": " + ex.Message);
                if (proxy.State == CommunicationState.Faulted)
                {
                    proxy = null;
                }
                handled = true;
            }
            else if (ex is CommunicationException)
            {
                Trace.WriteLine("Communication error is occured.");
                Trace.WriteLine(ex.Message);
                proxy = null;
                handled = true;
            }
        }

        private TService GetService()
        {
            return GetProxy().WcfChannel;
        }

        private WcfProxy<TService> GetProxy()
        {
            return proxy ?? (proxy = new WcfProxy<TService>(this.endpointConfigurationName));
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
