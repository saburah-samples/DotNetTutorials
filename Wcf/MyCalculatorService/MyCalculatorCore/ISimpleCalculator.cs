using System.ServiceModel;

namespace MyCalculatorCore
{
	[ServiceContract]
	public interface ISimpleCalculator
	{
		[OperationContract]
		int Add(int num1, int num2);

		[OperationContract]
		int Div(int num1, int num2);
	}
}
