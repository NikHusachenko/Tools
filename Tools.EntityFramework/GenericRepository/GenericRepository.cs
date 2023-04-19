using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tools.EntityFramework.GenericRepository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationContext _context;

		public GenericRepository(ApplicationContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public async Task Create(T entity)
		{
			await Table.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			Table.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public IQueryable<T> GetAll()
		{
			return Table.AsNoTracking();
		}

		public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
		{
			return Table.Where(expression);
		}

		public async Task<T> GetBy(Expression<Func<T, bool>> expression)
		{
			return await Table.FirstOrDefaultAsync(expression);
		}

		public async Task<T> GetById(long id)
		{
			return await Table.FindAsync(id);
		}

		public async Task Update(T entity)
		{
			Table.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
