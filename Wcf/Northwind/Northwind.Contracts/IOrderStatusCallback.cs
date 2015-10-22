using System.ServiceModel;

namespace Northwind.Contracts
{
	public interface IOrderStatusCallback
	{
		[OperationContract(IsOneWay = true)]
		void OnOrderStatusChanged(int orderId, OrderStatus status);
	}
}