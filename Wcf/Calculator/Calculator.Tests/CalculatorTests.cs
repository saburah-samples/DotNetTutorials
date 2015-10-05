using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Contracts;
using System.Diagnostics;
using System.ServiceModel;

namespace Calculator.Tests
{
	[TestClass]
	public class CalculatorTests
	{
		CalculatorClient service = new CalculatorClient();

		[TestMethod]
		public void NormalAdd()
		{
			Assert.IsNotNull(service, "Service is not created.");
			var result = service.Add(5, 5);
			Assert.AreEqual(10, result);
		}

		[TestMethod]
		public void NormalSub()
		{
			Assert.IsNotNull(service, "Service is not created.");
			var result = service.Sub(5, 5);
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void NormalMul()
		{
			Assert.IsNotNull(service, "Service is not created.");
			var result = service.Mul(5, 5);
			Assert.AreEqual(25, result);
		}

		[TestMethod]
		public void NormalDiv()
		{
			Assert.IsNotNull(service, "Service is not created.");
			var result = service.Div(5, 5);
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void FaultedDiv()
		{
			Assert.IsNotNull(service, "Service is not created.");
			try
			{
				var result = service.Div(5, 0);
				throw new DivideByZeroException();
			}
			catch (FaultException ex)
			{
				Trace.WriteLine(ex.Message);
			}
		}
	}
}
