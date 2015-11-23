using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Domain.Triggers.Repositories
{
	public abstract class RepositoryLocatorBase : IRepositoryLocator
	{
		private Dictionary<Type, object> repositoryMap = new Dictionary<Type, object>();

		public IList<TEntity> FindAll<TEntity>() where TEntity : class
		{
			return GetRepository<TEntity>().FindAll();
		}

		public TEntity GetById<TEntity>(long id) where TEntity : class
		{
			return GetRepository<TEntity>().GetById(id);
		}

		public void Insert<TEntity>(TEntity entity) where TEntity : class
		{
			GetRepository<TEntity>().Insert(entity);
        }

		public void Update<TEntity>(TEntity entity) where TEntity : class
		{
			GetRepository<TEntity>().Update(entity);
		}

		public void Delete<TEntity>(TEntity entity) where TEntity : class
		{
			GetRepository<TEntity>().Delete(entity);
		}

		public TResult GetCustomRepository<TEntity, TResult>()
			where TEntity : class
			where TResult : IRepository<TEntity>
		{
			return (TResult)GetRepository<TEntity>();
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			var entityType = typeof(TEntity);
            if (repositoryMap.ContainsKey(entityType))
			{
				return (IRepository<TEntity>)repositoryMap[entityType];
            }

			var result = CreateRepository<TEntity>();
			repositoryMap.Add(entityType, result);
			return result;
		}

		protected abstract IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
	}
}
