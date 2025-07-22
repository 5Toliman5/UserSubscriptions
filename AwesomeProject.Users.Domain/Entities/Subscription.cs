using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomeProject.Users.Domain.Entities
{
	[Table("subscriptions")]
	public class Subscription : IDomainEntity
	{
		[Column("id")]
		public int Id { get; private set; }

		[Column("type")]
		public SubscriptionType Type { get; set; }

		[Column("start_date")]
		public DateTime StartDate { get; set; }

		[Column("end_date")]
		public DateTime EndDate { get; set; }

		public ICollection<User> Users { get; private set; }

		public Subscription(SubscriptionType type, DateTime startDate, DateTime endDate)
		{
			Type = type;
			StartDate = startDate;
			EndDate = endDate;
			Users = [];
		}
	}

	public enum SubscriptionType : short
	{
		[Description("free")]
		Free = 0,

		[Description("trial")]
		Trial = 1,

		[Description("super")]
		Super = 2
	}
}
