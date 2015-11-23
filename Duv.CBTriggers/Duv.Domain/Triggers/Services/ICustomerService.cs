using Duv.Domain.Triggers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Services
{
	public interface ICustomerService
	{
		Customer GetCustomerByNumber(decimal number);
		CustomerDocument GetCustomerDocumentByNumber(long typeId, string series, string number);

		IList<CustomerDocument> FindCustomerDocuments(long customerId);
		IList<CustomerNumber> FindCustomerNumbers(long customerId);

		CustomerNumber CreateCustomerNumber(CustomerNumber number);
		CustomerDocument CreateCustomerDocument(CustomerDocument document);
	}
}
