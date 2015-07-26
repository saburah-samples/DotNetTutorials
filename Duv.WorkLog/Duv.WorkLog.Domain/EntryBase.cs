using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Domain
{
	public abstract class EntryBase
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime Modified { get; set; }
	}
}
