using System.Collections.Generic;

namespace WebSpikeApi.Core.Contracts
{
	public interface IRepository<T, TKey> where T : IEntity<TKey>
	{
		List<T> All();
		T GetById(TKey id);
		T Add(T entity);
		T Update(T entity);
		void Delete(T entity);
		void DeleteById(TKey id);
		void UnDelete(T entity);
		void UnDeleteById(TKey id);
		List<T> GetAllDeleted();

	}
}