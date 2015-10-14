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
        Order GetOrder(int orderId);

        [OperationContract]
        Order CreateOrder(Order order);

        [OperationContract]
        Order UpdateOrder(Order order);

        [OperationContract]
        void DeleteOrder(int orderId);

        [OperationContract]
        Order ApproveOrder(int orderId);

        [OperationContract]
        Order CompleteOrder(int orderId);
    }
}
