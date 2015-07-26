using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Core
{
	public class Task : EntryBase
	{
		public Project Project { get; set; }
		public Activity[] Activities { get; set; }
	}
}
