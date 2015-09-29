using MyCalculatorCore;
using System.ServiceModel;

namespace MyCalculatorServiceClient
{
	class SimpleCalculatorProxy : ClientBase<ISimpleCalculator>, ISimpleCalculator
	{
		public int Add(int num1, int num2)
		{
			return base.Channel.Add(num1, num2);
		}

		public int Div(int num1, int num2)
		{
			return base.Channel.Div(num1, num2);
		}
	}
}
