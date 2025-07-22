using AwesomeProject.Projects.Domain.Entities;

namespace AwesomeProject.Projects.Application.Repositories
{
	public interface IRepositoryBase<TEntity> where TEntity : class, IDomainEntity
	{
		Task CreateAsync(TEntity project);
		Task DeleteAsync(string id);
		Task<TEntity> GetByIdAsync(string id);
		Task UpdateAsync(TEntity project);
	}

}
