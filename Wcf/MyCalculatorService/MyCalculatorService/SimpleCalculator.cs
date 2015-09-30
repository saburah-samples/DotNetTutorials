using MyCalculatorCore;

namespace MyCalculatorService
{
	public class SimpleCalculator : ISimpleCalculator
	{
		public int Add(int num1, int num2)
		{
			return num1 + num2;
		}

		public int Div(int num1, int num2)
		{
			return num1 / num2;
		}
	}
}
