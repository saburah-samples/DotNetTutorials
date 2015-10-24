using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Northwind.Contracts
{
	[ServiceContract(CallbackContract = typeof(IOrderStatusCallback))]
	public interface IOrderStatusService : IContract
	{
		[OperationContract(IsOneWay = true)]
		void Subscribe();
		[OperationContract(IsOneWay = true)]
		void Unsubscribe();
	}
}
