using AwesomeProject.Users.Domain.Entities;

namespace AwesomeProject.Users.Application.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<int[]> GetUserIdsBySubscriptionType(SubscriptionType subscriptionType);
	}
}
