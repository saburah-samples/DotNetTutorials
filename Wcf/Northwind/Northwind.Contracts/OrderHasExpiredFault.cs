using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Contracts
{
	public class OrderHasExpiredFault : OrderFault
	{
		public DateTime? RequiredDate { get; set; }
	}
}
