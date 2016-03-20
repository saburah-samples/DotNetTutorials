using System.Linq;
using Duv.RequestManager.Model;

namespace Duv.RequestManager.Data
{
	/// <summary>
	/// Throws System.Data.DataException when an error of data access
	/// </summary>
	public interface IRequestRepository
	{
		long CreateRequest(Request request);
		void DeleteRequest(long requestId);
		IQueryable<Request> GetRequests();
		Request UpdateRequest(Request request);
	}
}