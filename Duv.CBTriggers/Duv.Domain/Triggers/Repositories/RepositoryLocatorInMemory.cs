using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Repositories
{
	public class RepositoryLocatorInMemory : RepositoryLocatorBase
	{
		protected override IRepository<TEntity> CreateRepository<TEntity>()
		{
			return new RepositoryInMemory<TEntity>();
		}
	}
}
