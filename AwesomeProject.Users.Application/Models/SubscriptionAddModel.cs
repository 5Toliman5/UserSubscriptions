using System.Text.Json.Serialization;

namespace AwesomeProject.Users.Application.Models
{
	public record SubscriptionAddModel
	{
		[JsonRequired]
		public required string Type { get; init; }

		[JsonRequired]
		public required DateTime StartDate { get; init; }

		[JsonRequired]
		public required DateTime EndDate { get; init; }
	}
}
