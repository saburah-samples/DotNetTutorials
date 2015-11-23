using System.Collections.Generic;

namespace Duv.Domain.Triggers.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		TEntity GetById(long id);

		IList<TEntity> FindAll();

		void Insert(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);
	}
}