using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.RequestManager.Model
{
	public class Request
	{
		public long Id { get; internal set; }
		public RequestType Type { get; set; }
		public RequestState State { get; set; }
		public RequestSource Source { get; set; }

		public object Data { get; set; }
	}
}
