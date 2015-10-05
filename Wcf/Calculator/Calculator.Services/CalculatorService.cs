using Calculator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Services
{
	public class CalculatorService : ICalculator
	{
		public double Add(double a, double b)
		{
			return a + b;
		}

		public double Sub(double a, double b)
		{
			return a - b;
		}

		public double Mul(double a, double b)
		{
			return a * b;
		}

		public double Div(double a, double b)
		{
			if (b == 0) throw new DivideByZeroException();
			return a / b;
		}
	}
}
