using MyCalculatorCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MyCalculatorServiceClient
{
    public class ServiceAdapter<TService> where TService : class
    {
        private WcfProxy<TService> proxy;

        public void Execute(Action<TService> command)
        {
            try
            {
                proxy = proxy ?? new WcfProxy<TService>();
                command.Invoke(proxy.WcfChannel);
            }
            catch (FaultException ex)
            {
                Trace.WriteLine("Communication channel is faulted.");
                Trace.WriteLine(ex.Code.Name + ": " + ex.Message);
                if (proxy.State == CommunicationState.Faulted)
                {
                    proxy = null;
                }
            }
            catch (CommunicationException ex)
            {
                Trace.WriteLine("Communication error is occured.");
                Trace.WriteLine(ex.Message);
                proxy = null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private class WcfProxy<TService> : ClientBase<TService> where TService : class
        {
            public TService WcfChannel
            {
                get
                {
                    return Channel;
                }
            }
        }
    }
}
