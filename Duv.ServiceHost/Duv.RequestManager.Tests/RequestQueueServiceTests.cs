using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Duv.RequestManager.Services;
using Duv.RequestManager.Model;

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

			request.Id = queue.SubmitRequest(request);
			Assert.IsNotNull(request);
			Assert.IsTrue(request.Id > 0);
			Assert.AreEqual(RequestState.InQueue, request.State);

			var canceled = queue.CancelRequest(request.Id);
			Assert.IsTrue(canceled);
		}
	}
}
