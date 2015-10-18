using System;
using System.Collections.Generic;

namespace Northwind.Contracts
{
	public class Order : DtoBase
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
				if (ShippedDate != null) return OrderStatus.Completed;
				if (OrderDate != null) return OrderStatus.InProgress;
				return OrderStatus.Draft;
			}
		}

		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
