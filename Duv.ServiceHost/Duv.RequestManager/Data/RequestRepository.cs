using Duv.RequestManager.Logging;
using Duv.RequestManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.RequestManager.Data
{
	class RequestRepository : IRequestRepository
	{
		private readonly ILogger logger;
		private readonly Dictionary<long, Request> requests;

		public RequestRepository()
		{
			logger = LoggerFactory.Create(this);
			requests = new Dictionary<long, Request>();
		}

		public IQueryable<Request> FindRequests()
		{
			return requests.Values.ToArray().AsQueryable();
		}

		public Request GetRequestById(long requestId)
		{
			try
			{
				if (requests.ContainsKey(requestId))
				{
					return requests[requestId];
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to get request with Id {0}.", requestId);
				throw new DataException(message, ex);
			}
		}

		public long CreateRequest(Request request)
		{
			try
			{
				request.Id = GetNewId();
				requests.Add(request.Id, request);
				return request.Id;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to create request. Request should not be null.");
				if (request != null)
					message = string.Format("Failed to create request with Id {0}.", request.Id);
				throw new DataException(message, ex);
			}
		}

		public Request UpdateRequest(Request request)
		{
			try
			{
				requests[request.Id] = request;
				return requests[request.Id];
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to update request. Request should not be null.");
				if (request != null)
					message = string.Format("Failed to update request with Id {0}.", request.Id);
				throw new DataException(message, ex);
			}
		}

		public void DeleteRequest(long requestId)
		{
			try
			{
				requests.Remove(requestId);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				var message = string.Format("Failed to delete request with Id {0}.", requestId);
				throw new DataException(message, ex);
			}
		}

		private long GetNewId()
		{
			return (requests.Any()) ? requests.Keys.Max() + 1 : 1;
		}
	}
}
