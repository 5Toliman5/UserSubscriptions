using AwesomeProject.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeProject.Users.Infrastructure.Repositories
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<User>()
				.HasOne(u => u.Subscription)
				.WithMany(s => s.Users)
				.HasForeignKey(u => u.SubscriptionId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Subscription>()
				.HasMany(s => s.Users)
				.WithOne(u => u.Subscription)
				.HasForeignKey(u => u.SubscriptionId);
		}
	}
}
