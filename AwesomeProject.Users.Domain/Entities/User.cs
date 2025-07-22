using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomeProject.Users.Domain.Entities
{
	[Table("users")]
	public class User : IDomainEntity
	{
		[Column("id")]
		public int Id { get; private set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("subscription_id")]
		public int? SubscriptionId { get; private set; }

		public Subscription Subscription { get; set; }

		public User(string name, string email)
		{
			Name = name;
			Email = email;
		}
	}
}
