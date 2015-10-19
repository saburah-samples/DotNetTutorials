using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Clients;
using Northwind.Contracts;
using Northwind.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;

namespace Northwind.Tests
{
	[TestClass]
	public class OrderServiceTests
	{
		private static ServiceHost host;
		private OrderServiceClient client = new OrderServiceClient();

		[ClassInitialize]
		public static void StartSvcHost(TestContext context)
		{
			host = new ServiceHost(typeof(OrderService));
			host.Open();
			Trace.WriteLine("Service started.");
		}

		[ClassCleanup]
		public static void StopSvcHost()
		{
			host.Close();
			Trace.WriteLine("Service stoped.");
		}

		[TestMethod]
		public void TestGetOrders()
		{
			var orders = client.GetOrders();
			Assert.IsTrue(orders != null, "GetOrders not should return null");
			Assert.IsTrue(orders.Any(), "GetOrders not should return empty list");
			Assert.IsTrue(orders.All(e => e.OrderID > 0), "OrderID should be greater 0");
			Assert.IsTrue(orders.Any(e => e.OrderDetails != null), "OrderDetails not should be null");
			Assert.IsTrue(orders.All(e => !e.OrderDetails.Any()), "OrderDetails should be empty");
		}

		[TestMethod]
		public void TestGetOrder()
		{
			var orderId = 10248;
			var actual = client.GetOrder(orderId);
			Assert.IsTrue(actual.OrderID == orderId, "OrderID should be {0}", orderId);
			Assert.IsTrue(actual.OrderDetails != null, "OrderDetails not should be null");
			Assert.IsTrue(actual.OrderDetails.Any(), "OrderDetails not should be empty");

			var orderDetail = actual.OrderDetails.FirstOrDefault();
			Assert.IsTrue(orderDetail.OrderID == actual.OrderID, "OrderID should be {0}", actual.OrderID);
			Assert.IsTrue(orderDetail.Product != null, "Product not should be null");
			Assert.IsTrue(orderDetail.ProductID == orderDetail.Product.ProductID, "ProductID should be equal Product.ProductID");
			Assert.IsTrue(orderDetail.ProductID > 0, "ProductID should be greater 0");
		}

		[TestMethod]
		public void TestCreateOrder()
		{
			var order = new Contracts.Order();
			order.RequiredDate = DateTime.Today.AddDays(14);
			order.CustomerID = "VINET";
			order.EmployeeID = 5;
			order.ShipCountry = "RU";
			order.OrderDetails.Add(new Contracts.OrderDetail()
			{
				ProductID = 1,
				Quantity = 10,
				UnitPrice = 15
			});
			var actual = client.CreateOrder(order);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.OrderID > 0, "OrderID should be greater 0");
			Assert.AreEqual(order.RequiredDate, actual.RequiredDate, "RequiredDate should be {0}", order.RequiredDate);
			Assert.AreEqual(null, actual.OrderDate, "RequiredDate should be null");
			Assert.AreEqual(null, actual.ShippedDate, "ShippedDate should be null");
			Assert.AreEqual(order.CustomerID, actual.CustomerID, "CustomerID should be {0}", order.CustomerID);
			Assert.AreEqual(order.EmployeeID, actual.EmployeeID, "EmployeeID should be {0}", order.EmployeeID);
			Assert.AreEqual(order.ShipCountry, actual.ShipCountry, "ShipCountry should be {0}", order.ShipCountry);

			Assert.IsTrue(order.OrderDetails != null, "OrderDetails not should be null");
			Assert.IsTrue(order.OrderDetails.Any(), "OrderDetails not should be empty");

			var orderDetail = actual.OrderDetails.FirstOrDefault();
			Assert.IsTrue(orderDetail.OrderID == actual.OrderID, "OrderID should be {0}", actual.OrderID);
			Assert.IsTrue(orderDetail.Product != null, "Product not should be null");
			Assert.IsTrue(orderDetail.ProductID == orderDetail.Product.ProductID, "ProductID should be equal Product.ProductID");
			Assert.IsTrue(orderDetail.ProductID > 0, "ProductID should be greater 0");
			Assert.AreEqual(1, orderDetail.ProductID, "ProductID should be {0}", 1);
			Assert.AreEqual(10, orderDetail.Quantity, "Quantity should be {0}", 10);
			Assert.AreEqual(15, orderDetail.UnitPrice, "UnitPrice should be {0}", 15);
		}

		[TestMethod]
		public void TestUpdateOrder()
		{
			var order = new Contracts.Order();
			order.RequiredDate = DateTime.Today.AddDays(14);
			order.CustomerID = "VINET";
			order.EmployeeID = 5;
			order.ShipCountry = "RU";
			order.OrderDetails.Add(new Contracts.OrderDetail()
			{
				ProductID = 1,
				Quantity = 10,
				UnitPrice = 15
			});

			order = client.CreateOrder(order);
			var orderId = order.OrderID;
			Assert.IsTrue(order.Status == Contracts.OrderStatus.Draft);
			Assert.IsTrue(order.OrderDetails.Count == 1);

			var item = new Contracts.OrderDetail()
			{
				ProductID = 2,
				Quantity = 20,
				UnitPrice = 25
			};

			var orderDetails = new List<OrderDetail>(order.OrderDetails);
			orderDetails.Add(item);
			order.OrderDetails = orderDetails;
			order = client.UpdateOrder(order);
			Assert.IsTrue(order.OrderDetails.Count == 2, "Order details should be added");

			orderDetails = new List<OrderDetail>(order.OrderDetails);
			item = orderDetails.First(e => e.ProductID == item.ProductID);
			orderDetails.Remove(item);
			order.OrderDetails = orderDetails;
			order = client.UpdateOrder(order);
			Assert.IsTrue(order.OrderDetails.Count == 1, "Order details should be deleted");
		}

		[TestMethod]
		public void TestDeleteOrder()
		{
			var order = new Contracts.Order();
			order.RequiredDate = DateTime.Today.AddDays(14);
			order.CustomerID = "VINET";
			order.EmployeeID = 5;
			order.ShipCountry = "RU";
			order.OrderDetails.Add(new Contracts.OrderDetail()
			{
				ProductID = 1,
				Quantity = 10,
				UnitPrice = 15
			});

			order = client.CreateOrder(order);
			var orderId = order.OrderID;
			Assert.IsTrue(order.Status == Contracts.OrderStatus.Draft);
			Assert.IsTrue(order.OrderDetails.Count == 1);

			client.DeleteOrder(orderId);
			var orders = client.GetOrders();
			Assert.IsFalse(orders.Any(e => e.OrderID == orderId));
		}

		[TestMethod]
		public void TestApproveOrder()
		{
			var order = new Contracts.Order();
			order.RequiredDate = DateTime.Today.AddDays(14);
			order.CustomerID = "VINET";
			order.EmployeeID = 5;
			order.ShipCountry = "RU";
			order.OrderDetails.Add(new Contracts.OrderDetail()
			{
				ProductID = 1,
				Quantity = 10,
				UnitPrice = 15
			});

			order = client.CreateOrder(order);
			var orderId = order.OrderID;
			Assert.IsTrue(order.Status == Contracts.OrderStatus.Draft);
			Assert.IsTrue(order.OrderDetails.Count == 1);

			order = client.ApproveOrder(orderId);
			Assert.IsTrue(order.Status == Contracts.OrderStatus.InProgress);
			Assert.IsTrue(order.OrderDate != null);

			try
			{
				var actual = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException when in progress order is updating");
			}
			catch (FaultException ex)
			{
				Assert.IsNotNull(ex, "UpdateOrder should throw FaultException when in progress order is updating");
			}

			client.DeleteOrder(orderId);
			var orders = client.GetOrders();
			Assert.IsFalse(orders.Any(e => e.OrderID == orderId), "Approved order should be deleted.");
		}

		[TestMethod]
		public void TestCompleteOrder()
		{
			var order = new Contracts.Order();
			order.RequiredDate = DateTime.Today.AddDays(14);
			order.CustomerID = "VINET";
			order.EmployeeID = 5;
			order.ShipCountry = "RU";
			order.OrderDetails.Add(new Contracts.OrderDetail()
			{
				ProductID = 1,
				Quantity = 10,
				UnitPrice = 15
			});

			order = client.CreateOrder(order);
			var orderId = order.OrderID;
			Assert.IsTrue(order.Status == Contracts.OrderStatus.Draft);
			Assert.IsTrue(order.OrderDetails.Count == 1);

			order = client.ApproveOrder(orderId);
			Assert.IsTrue(order.Status == Contracts.OrderStatus.InProgress);
			Assert.IsTrue(order.OrderDate != null);

			order = client.CompleteOrder(orderId);
			Assert.IsTrue(order.Status == Contracts.OrderStatus.Completed);
			Assert.IsTrue(order.ShippedDate != null);

			try
			{
				var actual = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException when completed order is updating");
			}
			catch (FaultException ex)
			{
				Assert.IsNotNull(ex, "UpdateOrder should throw FaultException when completed order is updating");
			}

			try
			{
				client.DeleteOrder(orderId);
				Assert.Fail("DeleteOrder should throw FaultException when completed order is deleting");
			}
			catch (FaultException ex)
			{
				Assert.IsNotNull(ex, "DeleteOrder should throw FaultException when completed order is deleting");
			}
			var orders = client.GetOrders();
			Assert.IsTrue(orders.Any(e => e.OrderID == orderId), "Completed order cannot be deleted");
		}

		[TestMethod]
		public void TestGetOrder_NotFoundFault()
		{
			try
			{
				var order = client.GetOrder(-1);
				Assert.Fail("GetOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
			catch (FaultException<OrderNotFoundFault>) { }
			catch (Exception)
			{
				Assert.Fail("GetOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
		}

		[TestMethod]
		public void TestUpdateOrder_NotFoundFault()
		{
			try
			{
				var order = new Contracts.Order() { OrderID = -1 };
				order = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
			catch (FaultException<OrderNotFoundFault>) { }
			catch (Exception)
			{
				Assert.Fail("UpdateOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
		}

		[TestMethod]
		public void TestDeleteOrder_NotFoundFault()
		{
			try
			{
				client.DeleteOrder(-1);
				Assert.Fail("DeleteOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
			catch (FaultException<OrderNotFoundFault>) { }
			catch (Exception)
			{
				Assert.Fail("DeleteOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
		}

		[TestMethod]
		public void TestApproveOrder_NotFoundFault()
		{
			try
			{
				var order = client.ApproveOrder(-1);
				Assert.Fail("ApproveOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
			catch (FaultException<OrderNotFoundFault>) { }
			catch (Exception)
			{
				Assert.Fail("ApproveOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
		}

		[TestMethod]
		public void TestCompleteOrder_NotFoundFault()
		{
			try
			{
				var order = client.CompleteOrder(-1);
				Assert.Fail("CompleteOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
			catch (FaultException<OrderNotFoundFault>) { }
			catch (Exception)
			{
				Assert.Fail("CompleteOrder should throw FaultException<OrderNotFoundFault> when order is not found");
			}
		}

		[TestMethod]
		public void TestCreateOrder_InvalidStatusFault()
		{
			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.OrderDate = DateTime.Today.AddDays(14);
				var actual = client.CreateOrder(order);
				Assert.Fail("CreateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("CreateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}

			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.OrderDate = DateTime.Today.AddDays(14);
				order.ShippedDate = DateTime.Today.AddDays(14);
				var actual = client.CreateOrder(order);
				Assert.Fail("CreateOrder should throw FaultException when completed order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("CreateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
		}

		[TestMethod]
		public void TestUpdateOrder_InvalidStatusFault()
		{
			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);

				order.OrderDate = DateTime.Today.AddDays(14);
				var actual = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}

			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);

				order.ShippedDate = DateTime.Today.AddDays(14);
				var actual = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}

			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);

				order.OrderDate = DateTime.Today.AddDays(14);
				order.ShippedDate = DateTime.Today.AddDays(14);
				var actual = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}

			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);
				order = client.ApproveOrder(order.OrderID);

				order.ShipCountry = "US";
				var actual = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}

			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);
				order = client.ApproveOrder(order.OrderID);
				order = client.CompleteOrder(order.OrderID);

				order.ShipCountry = "US";
				var actual = client.UpdateOrder(order);
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("UpdateOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
		}

		[TestMethod]
		public void TestDeleteOrder_InvalidStatusFault()
		{
			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);
				order = client.ApproveOrder(order.OrderID);
				order = client.CompleteOrder(order.OrderID);

				client.DeleteOrder(order.OrderID);
				Assert.Fail("DeleteOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("DeleteOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
		}

		[TestMethod]
		public void TestApproveOrder_InvalidStatusFault()
		{
			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);
				order = client.ApproveOrder(order.OrderID);

				order = client.ApproveOrder(order.OrderID);
				Assert.Fail("ApproveOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("ApproveOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}

			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);
				order = client.ApproveOrder(order.OrderID);
				order = client.CompleteOrder(order.OrderID);

				order = client.ApproveOrder(order.OrderID);
				Assert.Fail("ApproveOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("ApproveOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
		}

		[TestMethod]
		public void TestCompleteOrder_InvalidStatusFault()
		{
			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);

				order = client.CompleteOrder(order.OrderID);
				Assert.Fail("CompleteOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("CompleteOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}

			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(14);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);
				order = client.ApproveOrder(order.OrderID);
				order = client.CompleteOrder(order.OrderID);

				order = client.CompleteOrder(order.OrderID);
				Assert.Fail("CompleteOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
			catch (FaultException<InvalidOrderStatusFault>) { }
			catch (Exception)
			{
				Assert.Fail("CompleteOrder should throw FaultException<InvalidOrderStatusFault> when in progress order is creating");
			}
		}

		[TestMethod]
		public void TestApproveOrder_ExpiredFault()
		{
			try
			{
				var order = new Contracts.Order();
				order.RequiredDate = DateTime.Today.AddDays(-1);
				order.CustomerID = "VINET";
				order.EmployeeID = 5;
				order.ShipCountry = "RU";
				order.OrderDetails.Add(new Contracts.OrderDetail()
				{
					ProductID = 1,
					Quantity = 10,
					UnitPrice = 15
				});
				order = client.CreateOrder(order);

				order = client.ApproveOrder(order.OrderID);
				Assert.Fail("DeleteOrder should throw FaultException<OrderHasExpiredFault>");
			}
			catch (FaultException<OrderHasExpiredFault>) { }
			catch (Exception)
			{
				Assert.Fail("DeleteOrder should throw FaultException<OrderHasExpiredFault>");
			}
		}
	}
}
