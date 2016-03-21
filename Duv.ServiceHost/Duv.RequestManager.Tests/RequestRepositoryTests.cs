using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Duv.RequestManager.Data;
using Duv.RequestManager.Model;
using System.Linq;

namespace Duv.RequestManager.Tests
{
	[TestClass]
	public class RequestRepositoryTests
	{
		[TestMethod]
		public void SmokeTest()
		{
			var repository = ServiceLocator.GetService<IRequestRepository>();

			var requests = repository.FindRequests();
			Assert.IsNotNull(requests);

			var request = new Request
			{
				Type = RequestType.Start,
				Source = RequestSource.Manual
			};

			request.Id = repository.CreateRequest(request);
			Assert.IsTrue(request.Id > 0);
			Assert.IsFalse(requests.Any(e => e.Id == request.Id));
			requests = repository.FindRequests();
			Assert.IsTrue(requests.Any(e => e.Id == request.Id));
			request = repository.GetRequestById(request.Id);
			Assert.IsNotNull(request);

			request = repository.UpdateRequest(request);
			Assert.IsNotNull(request);

			repository.DeleteRequest(request.Id);
			requests = repository.FindRequests();
			Assert.IsFalse(requests.Any(e => e.Id == request.Id));
			request = repository.GetRequestById(request.Id);
			Assert.IsNull(request);
		}
	}
}
