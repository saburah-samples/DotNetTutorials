using System.Collections.Generic;
using System.ServiceModel;

namespace Northwind.Contracts
{
	[ServiceContract]
	public interface IOrderService : IContract
	{
		[OperationContract]
		IEnumerable<Order> GetOrders();

		[OperationContract]
		[FaultContract(typeof(OrderNotFoundFault))]
		Order GetOrder(int orderId);

		[OperationContract]
		[FaultContract(typeof(InvalidOrderStatusFault))]
		Order CreateOrder(Order order);

		[OperationContract]
		[FaultContract(typeof(OrderNotFoundFault))]
		[FaultContract(typeof(InvalidOrderStatusFault))]
		Order UpdateOrder(Order order);

		[OperationContract]
		[FaultContract(typeof(OrderNotFoundFault))]
		[FaultContract(typeof(InvalidOrderStatusFault))]
		void DeleteOrder(int orderId);

		[OperationContract]
		[FaultContract(typeof(OrderNotFoundFault))]
		[FaultContract(typeof(OrderHasExpiredFault))]
		[FaultContract(typeof(InvalidOrderStatusFault))]
		Order ApproveOrder(int orderId);

		[OperationContract]
		[FaultContract(typeof(OrderNotFoundFault))]
		[FaultContract(typeof(InvalidOrderStatusFault))]
		Order CompleteOrder(int orderId);
	}
}
