using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Queue
{
	public class RequestQueue
	{
		public void Send(Request request)
		{
			Repository.Create(request);
		}

		public Request Receive()
		{
			Request result = null;
			while (result == null)
			{
				var request = Repository.FindAll()
					.Where(e => e.Status == RequestStatus.InQueue)
					.FirstOrDefault();
				result = request;
			}
			return result;
		}

		public RequestRepository Repository { get; set; }
	}
}