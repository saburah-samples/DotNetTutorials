using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Models
{
	public class CustomerDocument
	{
		// id document info
		public long Id { get; set; }
		public long TypeId { get; set; }
		public string TypeCode { get; set; }
		public string Series { get; set; }
		public string Number { get; set; }
		public string IssueDate { get; set; }
		public string IssueLocation { get; set; }
		public string IssueAuthority { get; set; }

		// person info
		public long PersonId { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string FirstName { get; set; }
		public string BirthDate { get; set; }
		public string BirthPlace { get; set; }

		// customer reference
		public long CustomerId { get; set; }
	}
}
