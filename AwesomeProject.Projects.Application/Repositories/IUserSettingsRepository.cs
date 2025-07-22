using AwesomeProject.Projects.Domain.Entities;

namespace AwesomeProject.Projects.Application.Repositories
{
	public interface IUserSettingsRepository : IRepositoryBase<UserSettings>
	{
		Task<UserSettings> GetByUserIdAsync(int userId);
	}
}
