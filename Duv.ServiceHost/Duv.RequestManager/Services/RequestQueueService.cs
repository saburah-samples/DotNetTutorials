using Duv.RequestManager.Data;
using Duv.RequestManager.Logging;
using Duv.RequestManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.RequestManager.Services
{
	//TODO: fix concurrency troubles, the request should be locked for modifications until this operation has completed
	//TODO: about optimistic concurrency (locking) see: https://msdn.microsoft.com/en-us/library/aa0416cz(v=vs.110).aspx
	class RequestQueueService : IRequestQueueService
	{
		private readonly ILogger logger;
		private readonly IRequestRepository repository;

		public RequestQueueService()
		{
			logger = LoggerFactory.Create(this);
			repository = new RequestRepository();
		}

		public long SubmitRequest(Request request)
		{
			if (request == null)
			{
				var message = string.Format("Failed to submit request. Request should not be null.");
				throw new DomainException(message);
			}

			try
			{
				request.State = RequestState.InQueue;
				request.Id = repository.CreateRequest(request);
				return request.Id;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to submit request of type {0} from source {1}.", request.Type, request.Source);
				throw new DomainException(message, ex);
			}
		}

		public bool CancelRequest(long requestId)
		{
			Request existingRequest = repository.GetRequestById(requestId);
			if (existingRequest.State != RequestState.InQueue)
			{
				var message = string.Format("Failed to cancel request with Id {0}. It is already in state {1}.", requestId, existingRequest.State);
				throw new DomainException(message);
			}

			try
			{
				repository.DeleteRequest(requestId);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to cancel request with Id {0}.", requestId);
				throw new DomainException(message, ex);
			}
		}
	}
}
