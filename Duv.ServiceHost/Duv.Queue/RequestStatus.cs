using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Queue
{
	public enum RequestStatus
	{
		InQueue,
		InProcess,
		Completed,
		Failed
	}
}