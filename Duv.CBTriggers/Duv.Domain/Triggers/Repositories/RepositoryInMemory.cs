using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Repositories
{
	public class RepositoryInMemory<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private Dictionary<long, TEntity> cacheMap = new Dictionary<long, TEntity>();

		public IList<TEntity> FindAll()
		{
			return cacheMap.Values.ToList();
		}

		public TEntity GetById(long id)
		{
			return cacheMap[id];
		}

		public void Delete(TEntity entity)
		{
			var id = GetEntityId(entity);
			cacheMap.Remove(id);
		}

		public void Insert(TEntity entity)
		{
			var id = GetEntityId(entity);
			cacheMap.Add(id, entity);
        }

		public void Update(TEntity entity)
		{
			var id = GetEntityId(entity);
			cacheMap[id] = entity; // ?? or copy to same instance
		}

		private long GetEntityId(TEntity entity)
		{
			var instance = entity as IEntity;
			if (instance == null) throw new NotSupportedException("Interface IEntity is not supported.");
			return instance.Id;
		}
	}
}
