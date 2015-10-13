using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Northwind.Contracts
{
	[DataContract]
	public class Order
	{
		public Order()
		{
			this.OrderDetails = new HashSet<OrderDetail>();
		}

		public int OrderID { get; set; }
		public string CustomerID { get; set; }
		public int EmployeeID { get; set; }
		public int ShipperID { get; set; }

		public DateTime? OrderDate { get; set; }
		public DateTime? RequiredDate { get; set; }
		public DateTime? ShippedDate { get; set; }
		public decimal? Freight { get; set; }
		public string ShipName { get; set; }
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }

		public OrderStatus Status
		{
			get
			{
				if (OrderDate != null) return OrderStatus.InProgress;
				if (ShippedDate != null) return OrderStatus.Completed;
				return OrderStatus.Draft;
			}
		}

		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
