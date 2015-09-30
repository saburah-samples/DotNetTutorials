using MyCalculatorCore;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace MyCalculatorServiceClient
{
    class Program
    {
        private static SimpleCalculatorProxy proxy;

        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            Console.WriteLine("Client is running at " + DateTime.Now.ToString());
            var adapter = new ServiceAdapter<ISimpleCalculator>();
            adapter.Execute(s => Console.WriteLine("Sum of two numbers... 5+5 =" + s.Add(5, 5)));
            adapter.Execute(s => Console.WriteLine("Div of two numbers... 5/0 =" + s.Div(5, 0)));
            adapter.Execute(s => Console.WriteLine("Sum of two numbers... 5+5 =" + s.Add(5, 5)));
            adapter.Execute(s => Console.WriteLine("Div of two numbers... 5/0 =" + s.Div(5, 0)));

            Console.ReadLine();
        }
    }
}
