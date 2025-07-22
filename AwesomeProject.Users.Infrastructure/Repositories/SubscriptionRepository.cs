using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Domain.Entities;

namespace AwesomeProject.Users.Infrastructure.Repositories
{
	public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
	{
		public SubscriptionRepository(AppDbContext context) : base(context)
		{
		}

	}
}
