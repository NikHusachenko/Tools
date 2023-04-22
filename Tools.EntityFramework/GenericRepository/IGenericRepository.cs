using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tools.EntityFramework.GenericRepository
{
	public interface IGenericRepository<T> where T : class
	{
		DbSet<T> Table { get; }

		Task Create(T entity);
		Task Delete(T entity);
		Task Update(T entity);

		Task<T> GetById(long id);
		Task<T> GetBy(Expression<Func<T, bool>> expression);
		IQueryable<T> GetAll();
		IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

	}
}
