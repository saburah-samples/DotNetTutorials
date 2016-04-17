using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Queue
{
	public class RequestProcessor
	{
		public void ProcessRequest(Request request)
		{
			var handler = CreateRequestHandler(request);
			try
			{
				handler.OnProcess();
				handler.OnCompleted();
			}
			catch (Exception ex)
			{
				handler.OnFailed(ex);
			}
		}

		private RequestHandler CreateRequestHandler(Request request)
		{
			var handler = new RequestHandler(request);
			return handler;
		}
	}
}