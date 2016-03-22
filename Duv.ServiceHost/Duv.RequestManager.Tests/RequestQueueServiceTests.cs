using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Duv.RequestManager.Services;
using Duv.RequestManager.Model;
using System.Linq;

namespace Duv.RequestManager.Tests
{
	[TestClass]
	public class RequestQueueServiceTests
	{
		[TestMethod]
		public void SmokeTest()
		{
			var queue = ServiceLocator.GetService<IRequestQueueService>();

			var request = new Request
			{
				Type = RequestType.Start,
				State = RequestState.InQueue,
				Source = RequestSource.Manual,
			};

			var requests = queue.FindRequests();
			Assert.IsNotNull(requests);

			request.Id = queue.SubmitRequest(request);
			Assert.IsNotNull(request);
			Assert.IsTrue(request.Id > 0);
			Assert.IsFalse(requests.Any(e => e.Id == request.Id));
			requests = queue.FindRequests();
			Assert.IsTrue(requests.Any(e => e.Id == request.Id));
			request = queue.GetRequestById(request.Id);
			Assert.IsNotNull(request);

			var canceled = queue.CancelRequest(request.Id);
			Assert.IsTrue(canceled);
			requests = queue.FindRequests();
			Assert.IsFalse(requests.Any(e => e.Id == request.Id));
			request = queue.GetRequestById(request.Id);
			Assert.IsNull(request);
		}
	}
}
