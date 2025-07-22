using AwesomeProject.Common.Result;
using AwesomeProject.Users.Domain.Entities;

namespace AwesomeProject.Users.Application.Repositories
{
	public interface IRepository<TEntity> where TEntity : class, IDomainEntity
	{
		Task<List<TEntity>> GetAllAsync();
		Task<TEntity?> GetByIdAsync(int id);
		void Add(TEntity entity);
		Task<Result> DeleteByIdAsync(int id);
		Task SaveChangesAsync();
	}
}
