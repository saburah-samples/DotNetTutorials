using MyCalculatorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MyCalculatorServiceClient
{
	class MyCalculatorServiceProxy : ClientBase<ISimpleCalculator>, ISimpleCalculator
	{
		public int Add(int num1, int num2)
		{
			//Call base to do funtion
			return base.Channel.Add(num1, num2);
		}
	}
}
