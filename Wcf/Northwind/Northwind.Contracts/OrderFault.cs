using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Northwind.Contracts
{
	[DataContract]
	public abstract class OrderFault
	{
		[DataMember]
		public int OrderID { get; set; }
	}
}
