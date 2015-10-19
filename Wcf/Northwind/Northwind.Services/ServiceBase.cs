using Northwind.Services.Data;
using System;
using System.ServiceModel;

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
			try
			{
				using (var context = CreateContext())
				{
					command.Invoke(context);
				}
			}
			catch (FaultException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new FaultException(ex.Message, new FaultCode(ex.GetType().Name));
			}
		}

		protected TResult Execute<TResult>(Func<NorthwindContext, TResult> command)
		{
			try
			{
				using (var context = CreateContext())
				{
					return command.Invoke(context);
				}
			}
			catch (FaultException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new FaultException(ex.Message, new FaultCode(ex.GetType().Name));
			}
		}
	}
}
