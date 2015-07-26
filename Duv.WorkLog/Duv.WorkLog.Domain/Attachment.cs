using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Domain
{
	public class Attachment : EntryBase
	{
		public EntryBase Target { get; set; }
	}
}
