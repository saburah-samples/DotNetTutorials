using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Models
{
	public class Customer
	{
		long Id { get; set; }
		public IList<CustomerNumber> Numbers { get; set; }
		public IList<CustomerDocument> Documents { get; set; }
	}
}
