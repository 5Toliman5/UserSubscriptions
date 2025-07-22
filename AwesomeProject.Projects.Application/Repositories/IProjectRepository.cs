using AwesomeProject.Projects.Domain.Entities;

namespace AwesomeProject.Projects.Application.Repositories
{
	public interface IProjectRepository : IRepositoryBase<Project>
	{
		Task<List<Project>> GetByUserIdAsync(int userId);
		Task<List<Project>> GetByUserIdsAsync(int[] userIds);
	}
}
