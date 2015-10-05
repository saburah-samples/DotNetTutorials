using Calculator.Services;
using System;
using System.ServiceModel;

namespace Calculator.SvcHost
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				using (var host = new ServiceHost(typeof(CalculatorService)))
				{
					host.Open();
					Console.WriteLine("Service started.");
					Console.ReadLine();
					host.Close();
					Console.WriteLine("Service stoped.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("ERROR: " + ex.Message);
				Console.ReadLine();
			}
		}
	}
}
