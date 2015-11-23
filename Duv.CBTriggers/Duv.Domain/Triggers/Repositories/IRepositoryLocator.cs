using System.Collections.Generic;
using Duv.Domain.Triggers.Models;

namespace Duv.Domain.Triggers.Repositories
{
	public interface IRepositoryLocator
	{
		IList<TEntity> FindAll<TEntity>() where TEntity : class;

		TEntity GetById<TEntity>(long id) where TEntity : class;

		void Insert<TEntity>(TEntity entity) where TEntity : class;

		void Update<TEntity>(TEntity entity) where TEntity : class;

		void Delete<TEntity>(TEntity entity) where TEntity : class;

		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

		TResult GetCustomRepository<TEntity, TResult>()
			where TEntity : class
			where TResult : IRepository<TEntity>;
	}
}