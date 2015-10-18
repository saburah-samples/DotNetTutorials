using Northwind.Services.Data;
using System;

namespace Northwind.Services
{
	public abstract class ServiceBase
	{
		protected NorthwindContext CreateContext()
		{
			return new NorthwindContext();
		}

		protected void Execute(Action<NorthwindContext> command)
		{
			using (var context = CreateContext())
			{
				command.Invoke(context);
			}
		}

		protected TResult Execute<TResult>(Func<NorthwindContext, TResult> command)
		{
			using (var context = CreateContext())
			{
				return command.Invoke(context);
			}
		}
	}
}
