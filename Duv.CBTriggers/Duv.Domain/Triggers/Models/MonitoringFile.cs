using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Models
{
	public class MonitoringFile
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public long Size { get; set; }
		public DateTime? Modified { get; set; }
		public DateTime? Loaded { get; set; }
		public bool IsLoaded { get; set; }

		public IList<MonitoringDocument> Documents { get; set; }
	}
}
