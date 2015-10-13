using Northwind.Services.Data;

namespace Northwind.Services
{
	public abstract class ServiceBase
	{
		protected NorthwindContext CreateContext()
		{
			return new NorthwindContext();
		}
	}
}
