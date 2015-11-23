using System;

namespace Duv.Domain.Triggers.Models
{
	public class CustomerNumber
	{
		// customer number
		public long Id { get; set; }
		public decimal Number { get; set; }
		// customer reference
		public long CustomerId { get; set; }

		// person info
		public long PersonId { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string FirstName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string BirthPlace { get; set; }
	}
}