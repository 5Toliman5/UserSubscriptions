using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeProject.Users.Infrastructure.Repositories
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<int[]> GetUserIdsBySubscriptionType(SubscriptionType subscriptionType)
		{
			return await DbSet
				.Where(u => u.Subscription.Type == subscriptionType)
				.Select(u => u.Id)
				.ToArrayAsync();
		}

		public Task<User?> GetByEmailAsync(string email)
		{
			return DbSet.FirstOrDefaultAsync(u => u.Email == email);
		}

		protected override IQueryable<User> SelectAllQueryBase => 
			base.SelectAllQueryBase
			.Include(u => u.Subscription);
		protected override IQueryable<User> SelectSingleQueryBase =>
			base.SelectSingleQueryBase
			.Include(u => u.Subscription);
	}
}
