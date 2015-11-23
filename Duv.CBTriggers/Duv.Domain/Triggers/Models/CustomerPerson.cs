using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Models
{
	public class CustomerPerson
	{
		// person info 
		public long Id { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string FirstName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string BirthPlace { get; set; }

		// customer reference
		public long CustomerId { get; set; }

		public IList<CustomerNumber> CustomerNumbers { get; set; }
	}
}
