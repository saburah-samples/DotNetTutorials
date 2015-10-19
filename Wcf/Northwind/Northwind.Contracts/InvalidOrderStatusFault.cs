using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Contracts
{
	public class InvalidOrderStatusFault:OrderFault
	{
		public OrderStatus Status { get; set; }
	}
}
