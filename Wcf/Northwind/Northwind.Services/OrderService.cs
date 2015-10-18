using AutoMapper;
using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Services
{
	public class OrderService : ServiceBase, IOrderService
	{
		private const string ErrorOrderNotFound = "Order {0} is not found.";
		private const string ErrorOrderHasExpiredOn = "Order {0} has expired on {1}.";
		private const string ErrorInvalidOrderStatusForCreate = "Cannot create order in {0} status.";
		private const string ErrorInvalidOrderStatusForUpdate = "Cannot update order in {0} status.";
		private const string ErrorOrderHasInvalidStatusForUpdate = "Order {0} has invalid status {1} for update, should be in Draft.";
		private const string ErrorOrderHasInvalidStatusForDelete = "Order {0} has invalid status {1} for delete, should be in Draft or InProgress.";
		private const string ErrorOrderHasInvalidStatusForApprove = "Order {0} has invalid status {1} for approve, should be in Draft.";
		private const string ErrorOrderHasInvalidStatusForComplete = "Order {0} has invalid status {1} for complete, should be in InProgress.";

		static OrderService()
		{
			//read: DB -> Svc

			Mapper.CreateMap<Data.Product, Product>();

			Mapper.CreateMap<Data.Order_Detail, OrderDetail>();

			Mapper.CreateMap<Data.Order, Order>()
				.ForMember(d => d.CustomerID, m => m.MapFrom(s => s.Customer.CustomerID))
				.ForMember(d => d.EmployeeID, m => m.MapFrom(s => s.Employee.EmployeeID))
				.ForMember(d => d.Status, m => m.Ignore())
				.ForMember(d => d.OrderDetails, m => m.Ignore());

			//write: Svc -> DB

			Mapper.CreateMap<OrderDetail, Data.Order_Detail>()
				.ForMember(d => d.OrderID, m => m.MapFrom(s => s.OrderID))
				.ForMember(d => d.ProductID, m => m.MapFrom(s => s.ProductID))
				.ForMember(d => d.Product, m => m.Ignore());

			Mapper.CreateMap<Order, Data.Order>()
				.ForMember(d => d.OrderDate, m => m.Ignore())
				.ForMember(d => d.ShippedDate, m => m.Ignore())
				.ForMember(d => d.Order_Details, m => m.MapFrom(s => s.OrderDetails));
		}

		public IEnumerable<Order> GetOrders()
		{
			return Execute(context => GetOrders(context));
		}

		public Order GetOrder(int orderId)
		{
			return Execute(context => GetOrder(context, orderId));
		}

		public Order CreateOrder(Order order)
		{
			var orderId = Execute(context => CreateOrder(context, order));
			return Execute(context => GetOrder(context, orderId));
		}

		public Order UpdateOrder(Order order)
		{
			Execute(context => UpdateOrder(context, order));
			return Execute(context => GetOrder(context, order.OrderID));
		}

		public void DeleteOrder(int orderId)
		{
			Execute(context => DeleteOrder(context, orderId));
		}

		public Order ApproveOrder(int orderId)
		{
			Execute(context => ApproveOrder(context, orderId));
			return Execute(context => GetOrder(context, orderId));
		}

		public Order CompleteOrder(int orderId)
		{
			Execute(context => CompleteOrder(context, orderId));
			return Execute(context => GetOrder(context, orderId));
		}

		private IEnumerable<Order> GetOrders(Data.NorthwindContext context)
		{
			var orders = context.Orders.ToArray();
			var result = Mapper.Map<IEnumerable<Order>>(orders);
			return result;
		}

		private Order GetOrder(Data.NorthwindContext context, int orderId)
		{
			var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
			if (instance == null)
			{
				var message = string.Format(ErrorOrderNotFound, orderId);
				throw new InvalidOperationException(message);
			}

			var orderDetails = instance.Order_Details.ToArray();
			var result = Mapper.Map<Order>(instance);
			Mapper.Map(orderDetails, result.OrderDetails);
			return result;
		}

		private int CreateOrder(Data.NorthwindContext context, Order order)
		{
			if (order == null)
			{
				throw new ArgumentNullException("order");
			}
			if (order.Status != OrderStatus.Draft)
			{
				var message = string.Format(ErrorInvalidOrderStatusForCreate, order.Status);
				throw new InvalidOperationException(message);
			}

			var instance = context.Orders.Create();

			Mapper.Map(order, instance);
			instance.Customer = context.Customers.First(e => e.CustomerID == order.CustomerID);
			instance.Employee = context.Employees.First(e => e.EmployeeID == order.EmployeeID);
			instance.Order_Details.ToList().ForEach(e =>
			{
				e.Product = context.Products.First(p => p.ProductID == e.ProductID);
			});
			context.Orders.Add(instance);
			context.SaveChanges();

			return instance.OrderID;
		}

		private void UpdateOrder(Data.NorthwindContext context, Order order)
		{
			if (order == null)
			{
				throw new ArgumentNullException("order");
			}
			if (order.Status != OrderStatus.Draft)
			{
				var message = string.Format(ErrorInvalidOrderStatusForUpdate, order.Status);
				throw new InvalidOperationException(message);
			}

			var orderId = order.OrderID;
			var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
			if (instance == null)
			{
				var message = string.Format(ErrorOrderNotFound, orderId);
				throw new InvalidOperationException(message);
			}
			var currentOrder = Mapper.Map<Order>(instance);
			if (currentOrder.Status != OrderStatus.Draft)
			{
				var message = string.Format(ErrorOrderHasInvalidStatusForUpdate, currentOrder.OrderID, currentOrder.Status);
				throw new InvalidOperationException(message);
			}

			Mapper.Map(order, instance);
			instance.Customer = context.Customers.First(e => e.CustomerID == order.CustomerID);
			instance.Employee = context.Employees.First(e => e.EmployeeID == order.EmployeeID);
			instance.Order_Details.ToList().ForEach(e =>
			{
				e.Product = context.Products.First(p => p.ProductID == e.ProductID);
			});
			context.SaveChanges();
		}

		private void DeleteOrder(Data.NorthwindContext context, int orderId)
		{
			var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
			if (instance == null)
			{
				var message = string.Format(ErrorOrderNotFound, orderId);
				throw new InvalidOperationException(message);
			}
			var currentOrder = Mapper.Map<Order>(instance);
			if (currentOrder.Status == OrderStatus.Completed)
			{
				var message = string.Format(ErrorOrderHasInvalidStatusForDelete, currentOrder.OrderID, currentOrder.Status);
				throw new InvalidOperationException(message);
			}

			instance.Order_Details.Clear();
			context.Orders.Remove(instance);
			context.SaveChanges();
		}

		private void ApproveOrder(Data.NorthwindContext context, int orderId)
		{
			var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
			if (instance == null)
			{
				var message = string.Format(ErrorOrderNotFound, orderId);
				throw new InvalidOperationException(message);
			}
			var currentOrder = Mapper.Map<Order>(instance);
			if (currentOrder.Status != OrderStatus.Draft)
			{
				var message = string.Format(ErrorOrderHasInvalidStatusForApprove, currentOrder.OrderID, currentOrder.Status);
				throw new InvalidOperationException(message);
			}
			if (currentOrder.RequiredDate < DateTime.Today)
			{
				var message = string.Format(ErrorOrderHasExpiredOn, currentOrder.OrderID, currentOrder.RequiredDate);
				throw new InvalidOperationException(message);
			}

			instance.OrderDate = DateTime.Today;
			context.SaveChanges();
		}

		private void CompleteOrder(Data.NorthwindContext context, int orderId)
		{
			var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
			if (instance == null)
			{
				var message = string.Format(ErrorOrderNotFound, orderId);
				throw new InvalidOperationException(message);
			}
			var currentOrder = Mapper.Map<Order>(instance);
			if (currentOrder.Status != OrderStatus.InProgress)
			{
				var message = string.Format(ErrorOrderHasInvalidStatusForComplete, currentOrder.OrderID, currentOrder.Status);
				throw new InvalidOperationException(message);
			}

			instance.ShippedDate = DateTime.Today;
			context.SaveChanges();
		}
	}
}
