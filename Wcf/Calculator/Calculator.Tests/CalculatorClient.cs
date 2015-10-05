﻿using Calculator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Calculator.Tests
{
	class CalculatorClient : ClientBase<ICalculator>, ICalculator
	{
		public double Add(double a, double b)
		{
			return base.Channel.Add(a, b);
		}

		public double Sub(double a, double b)
		{
			return base.Channel.Sub(a, b);
		}

		public double Mul(double a, double b)
		{
			return base.Channel.Mul(a, b);
		}

		public double Div(double a, double b)
		{
			return base.Channel.Div(a, b);
		}
	}
}
