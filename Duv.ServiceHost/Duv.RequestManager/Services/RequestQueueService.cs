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
	class RequestQueueService
	{
		private readonly ILogger logger;
		private readonly IRequestRepository repository;

		public RequestQueueService()
		{
			logger = LoggerFactory.Create(this);
			repository = new RequestRepository();
		}

		public long CreateRequest(RequestType type, RequestSource source)
		{
			try
			{
				var request = new Request
				{
					Type = type,
					State = RequestState.Draft,
					Source = source
				};
				request.Id = repository.CreateRequest(request);
				return request.Id;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to create request of type {0} from source {1}.", type, source);
				throw new DomainException(message, ex);
			}
		}

		public RequestState SubmitRequest(Request request)
		{
			if (request == null)
			{
				var message = string.Format("Failed to submit request. Request should not be null.");
				throw new DomainException(message);
			}

			try
			{
				var requestState = repository.GetRequests()
					.Single(e => e.Id == request.Id)
					.State;
				if (requestState != RequestState.Draft)
				{
					var message = string.Format("Failed to submit request with Id = {0}. It is already in state {1}.", request.Id, requestState);
					throw new DomainException(message);
				}

				request.State = RequestState.InQueue;
				request = repository.UpdateRequest(request);
				return request.State;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to submit request with Id {0}.", request.Id);
				throw new DomainException(message, ex);
			}
		}

		public bool CancelRequest(long requestId)
		{
			try
			{
				var requestState = repository.GetRequests()
					.Single(e => e.Id == requestId)
					.State;
				if (requestState != RequestState.Draft)
				{
					var message = string.Format("Failed to cancel request with Id = {0}. It is already in state {1}.", requestId, requestState);
					throw new DomainException(message);
				}

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
