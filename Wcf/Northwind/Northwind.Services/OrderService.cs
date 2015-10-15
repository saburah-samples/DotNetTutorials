using AutoMapper;
using Northwind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Services
{
	public class OrderService : ServiceBase, IOrderService
	{
		private const string InvalidOperationOrderNotFound = "{0} is failed. Order {1} is not found";
		private const string InvalidOperationOrderNotUpdated = "{0} is failed. Order {1} in status {2} cannot be updated.";
		private const string InvalidOperationOrderNotDeleted = "{0} is failed. Order {1} in status {2} cannot be deleted.";
		private const string InvalidOperationOrderNotApproved = "{0} is failed. Order {1} in status {2} cannot be approved.";
		private const string InvalidOperationOrderNotCompleted = "{0} is failed. Order {1} in status {2} cannot be completed.";
		private const string InvalidOperationOrderHasExpired = "{0} is failed. Order {1} has expired on {2}";

		static OrderService()
		{
			//DB -> Svc

			Mapper.CreateMap<Data.Product, Product>();

			Mapper.CreateMap<Data.Order_Detail, OrderDetail>();

			Mapper.CreateMap<Data.Order, Order>()
				.ForMember(d => d.Status, m => m.Ignore())
				.ForMember(d => d.OrderDetails, m => m.Ignore());
				//.ForMember(d => d.OrderDetails, m => m.MapFrom(s => s.Order_Details));

			//Svc -> DB

			Mapper.CreateMap<OrderDetail, Data.Order_Detail>();

			Mapper.CreateMap<Order, Data.Order>()
				.ForMember(d => d.OrderDate, m => m.Ignore())
				.ForMember(d => d.ShippedDate, m => m.Ignore())
				.ForMember(d => d.Order_Details, m => m.MapFrom(s => s.OrderDetails));
		}

		public IEnumerable<Order> GetOrders()
		{
			using (var context = CreateContext())
			{
				var orders = context.Orders.ToArray();
				var result = Mapper.Map<IEnumerable<Order>>(orders);
				return result;
			}
		}

		public Order GetOrder(int orderId)
		{
			using (var context = CreateContext())
			{
				var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
				if (instance == null)
				{
					throw new InvalidOperationException(string.Format(InvalidOperationOrderNotFound, "GetOrder", orderId));
				}

				var orderDetails = instance.Order_Details.ToArray();
				var result = Mapper.Map<Order>(instance);
				Mapper.Map(orderDetails, result.OrderDetails);
				return result;
			}
		}

		public Order CreateOrder(Order order)
		{
			if (order == null)
			{
				throw new ArgumentNullException("order");
			}
			using (var context = CreateContext())
			{
				var instance = context.Orders.Create();

				Mapper.Map(order, instance);
				context.SaveChanges();

				return Mapper.Map<Order>(instance);
			}
		}

		public Order UpdateOrder(Order order)
		{
			if (order == null)
			{
				throw new ArgumentNullException("order");
			}
			var orderId = order.OrderID;

			using (var context = CreateContext())
			{
				var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
				if (instance == null)
				{
					throw new InvalidOperationException(string.Format(InvalidOperationOrderNotFound, "UpdateOrder", orderId));
				}
				CheckForUpdateOrder(Mapper.Map<Order>(instance));

				Mapper.Map(order, instance);
				context.SaveChanges();

				return Mapper.Map<Order>(instance);
			}
		}

		public void DeleteOrder(int orderId)
		{
			using (var context = CreateContext())
			{
				var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
				if (instance == null)
				{
					throw new InvalidOperationException(string.Format(InvalidOperationOrderNotFound, "DeleteOrder", orderId));
				}
				CheckForDeleteOrder(Mapper.Map<Order>(instance));

				context.Orders.Remove(instance);
				context.SaveChanges();
			}
		}

		public Order ApproveOrder(int orderId)
		{
			using (var context = CreateContext())
			{
				var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
				if (instance == null)
				{
					throw new InvalidOperationException(string.Format(InvalidOperationOrderNotFound, "ApproveOrder", orderId));
				}
				CheckForApproveOrder(Mapper.Map<Order>(instance));

				instance.OrderDate = DateTime.Today;
				context.SaveChanges();

				return Mapper.Map<Order>(instance);
			}
		}

		public Order CompleteOrder(int orderId)
		{
			using (var context = CreateContext())
			{
				var instance = context.Orders.FirstOrDefault(e => e.OrderID == orderId);
				if (instance == null)
				{
					throw new InvalidOperationException(string.Format(InvalidOperationOrderNotFound, "CompleteOrder", orderId));
				}
				CheckForCompleteOrder(Mapper.Map<Order>(instance));

				instance.ShippedDate = DateTime.Today;
				context.SaveChanges();

				return Mapper.Map<Order>(instance);
			}
		}

		private void CheckForUpdateOrder(Order order)
		{
			var orderId = order.OrderID;
			if (order.Status != OrderStatus.Draft)
			{
				throw new InvalidOperationException(string.Format(InvalidOperationOrderNotUpdated, "UpdateOrder", orderId, order.Status));
			}
		}

		private void CheckForDeleteOrder(Order order)
		{
			var orderId = order.OrderID;
			if (order.Status == OrderStatus.Completed)
			{
				throw new InvalidOperationException(string.Format(InvalidOperationOrderNotDeleted, "DeleteOrder", orderId, order.Status));
			}
		}

		private void CheckForApproveOrder(Order order)
		{
			var orderId = order.OrderID;
			if (order.Status != OrderStatus.Draft)
			{
				throw new InvalidOperationException(string.Format(InvalidOperationOrderNotApproved, "ApproveOrder", orderId, order.Status));
			}
			if (order.RequiredDate < DateTime.Today)
			{
				throw new InvalidOperationException(string.Format(InvalidOperationOrderHasExpired, "ApproveOrder", orderId, order.RequiredDate));
			}
		}

		private void CheckForCompleteOrder(Order order)
		{
			var orderId = order.OrderID;
			if (order.Status != OrderStatus.InProgress)
			{
				throw new InvalidOperationException(string.Format(InvalidOperationOrderNotCompleted, "CompleteOrder", orderId, order.Status));
			}
		}
	}
}
