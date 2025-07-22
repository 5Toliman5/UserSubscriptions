using AwesomeProject.Projects.Application.Repositories;
using AwesomeProject.Projects.Domain.Entities;
using MongoDB.Driver;

namespace AwesomeProject.Projects.Infrastructure.Repositories
{
	public class UserSettingsRepository : RepositoryBase<UserSettings>, IUserSettingsRepository
	{
		protected override string CollectionName => "userSettings";
		public UserSettingsRepository(IMongoDatabase database) : base(database)
		{
		}

		public Task<UserSettings> GetByUserIdAsync(int userId)
		{
			return Collection.Find(x => x.UserId == userId).SingleOrDefaultAsync();
		}
	}
}
