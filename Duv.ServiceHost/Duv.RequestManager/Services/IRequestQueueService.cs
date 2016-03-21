using Duv.RequestManager.Model;

namespace Duv.RequestManager.Services
{
	public interface IRequestQueueService
	{
		long SubmitRequest(Request request);
		bool CancelRequest(long requestId);
	}
}