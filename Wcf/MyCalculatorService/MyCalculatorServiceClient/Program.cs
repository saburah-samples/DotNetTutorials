using MyCalculatorCore;
using System;
using System.ServiceModel;

namespace MyCalculatorServiceClient
{
    class Program
    {
        private static SimpleCalculatorProxy proxy;

        static void Main(string[] args)
        {

            Console.WriteLine("Client is running at " + DateTime.Now.ToString());
            Execute(s => Console.WriteLine("Sum of two numbers... 5+5 =" + s.Add(5, 5)));
            Execute(s => Console.WriteLine("Div of two numbers... 5/0 =" + s.Div(5, 0)));
            Execute(s => Console.WriteLine("Sum of two numbers... 5+5 =" + s.Add(5, 5)));
            Execute(s => Console.WriteLine("Div of two numbers... 5/0 =" + s.Div(5, 0)));

            Console.ReadLine();
        }

        static void Execute(Action<ISimpleCalculator> command)
        {
            if (proxy == null)
            {
                proxy = new SimpleCalculatorProxy();
                Console.WriteLine("New communication channel is created.");
            }

            try
            {
                command.Invoke(proxy);
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Communication channel is faulted.");
                Console.WriteLine(ex.GetType().FullName + "\n" + ex.Code.Name + ": " + ex.Message);
                if (proxy.State == CommunicationState.Faulted)
                {
                    proxy = null;
                }
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine("Communication error is occured.");
                Console.WriteLine(ex.GetType().FullName + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
