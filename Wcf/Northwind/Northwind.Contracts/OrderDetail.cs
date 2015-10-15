using System.Runtime.Serialization;

namespace Northwind.Contracts
{
	public class OrderDetail : DtoBase
	{
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
		public float Discount { get; set; }

		public virtual Product Product { get; set; }
	}
}
