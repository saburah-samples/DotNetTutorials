using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Core
{
	public class Project : EntryBase
	{
		public Task[] Tasks { get; set; }
		public Activity[] Activities { get; set; }
	}
}
