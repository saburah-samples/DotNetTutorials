using System.ServiceModel;

namespace Northwind.Contracts
{
	public delegate void OrderStatusChangedEventHandler(int orderId, OrderStatus status);

	public interface IOrderStatusCallback
	{
		[OperationContract(IsOneWay = true)]
		void OnOrderStatusChanged(int orderId, OrderStatus status);
	}
}