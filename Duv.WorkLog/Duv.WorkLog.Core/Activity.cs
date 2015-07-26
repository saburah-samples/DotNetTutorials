using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Core
{
	public class Activity : EntryBase
	{
		public Project Project { get; set; }
		public EntryBase Source { get; set; }
	}
}
