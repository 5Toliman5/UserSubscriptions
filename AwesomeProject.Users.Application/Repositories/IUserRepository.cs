using AwesomeProject.Users.Domain.Entities;

namespace AwesomeProject.Users.Application.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> GetByEmailAsync(string email);
		Task<int[]> GetUserIdsBySubscriptionType(SubscriptionType subscriptionType);
	}
}
