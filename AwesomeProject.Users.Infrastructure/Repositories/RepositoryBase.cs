using AwesomeProject.Common.Result;
using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeProject.Users.Infrastructure.Repositories
{
	public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IDomainEntity
	{
		private readonly AppDbContext _context;

		public RepositoryBase(AppDbContext context)
		{
			_context = context;
		}

		public Task<List<TEntity>> GetAllAsync()
		{
			return SelectAllQueryBase.ToListAsync();
		}

		public Task<TEntity?> GetByIdAsync(int id)
		{
			return DbSet.FirstOrDefaultAsync(x => x.Id == id);
		}

		public void Add(TEntity entity)
		{
			DbSet.Add(entity);
		}

		public async Task<Result> DeleteByIdAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if (entity is null)
				return Result.Failure(ResultErrorType.NotFound, $"Entity of type {typeof(TEntity).Name} with ID {id} not found.");
			DbSet.Remove(entity);
			return Result.Success();
		}

		public Task SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}

		protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

		protected virtual IQueryable<TEntity> SelectAllQueryBase => DbSet.AsNoTracking();

		protected virtual IQueryable<TEntity> SelectSingleQueryBase => DbSet;
	}
}
