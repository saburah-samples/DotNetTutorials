using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Queue
{
	public class RequestHandler
	{
		private Request request;

		public RequestHandler(Request request)
		{
			this.request = request;
		}

		internal void OnProcess()
		{
			throw new NotImplementedException();
		}

		internal void OnCompleted()
		{
			request.Status = RequestStatus.Completed;
			Repository.Update(request);
		}

		internal void OnFailed(Exception ex)
		{
			request.Status = RequestStatus.Failed;
			Repository.Update(request);
		}

		public RequestRepository Repository { get; set; }
	}
}