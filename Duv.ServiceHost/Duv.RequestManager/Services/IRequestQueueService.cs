using Duv.RequestManager.Model;
using System.Collections.Generic;

namespace Duv.RequestManager.Services
{
	public interface IRequestQueueService
	{
		long SubmitRequest(Request request);
		bool CancelRequest(long requestId);
		IEnumerable<Request> FindRequests();
		Request GetRequestById(long requestId);
	}
}