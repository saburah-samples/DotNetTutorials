using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Core
{
	public class EntryBase
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedAt { get; set; }
	}
}
