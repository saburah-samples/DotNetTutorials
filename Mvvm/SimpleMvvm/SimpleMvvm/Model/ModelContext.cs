using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMvvm.Model
{
	public class ModelContext
	{
		private static Lazy<ModelContext> defaultInstance = new Lazy<ModelContext>(CreateModelContext, true);

		private static ModelContext CreateModelContext()
		{
			return new ModelContext();
		}

		public static ModelContext Default { get { return defaultInstance.Value; } }

		public ICustomerRepository CustomerRepository { get; set; }
	}
}
