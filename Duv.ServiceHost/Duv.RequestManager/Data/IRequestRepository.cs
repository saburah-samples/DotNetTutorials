using System.Linq;
using Duv.RequestManager.Model;
using System.Collections.Generic;

namespace Duv.RequestManager.Data
{
	/// <summary>
	/// Throws System.Data.DataException when an error of data access
	/// </summary>
	public interface IRequestRepository
	{
		long CreateRequest(Request request);
		void DeleteRequest(long requestId);
		Request UpdateRequest(Request request);

		IQueryable<Request> FindRequests();
		Request GetRequestById(long id);
	}
}