
using System.Text.Json.Serialization;

namespace AwesomeProject.Users.Application.Models
{
	public record SubscriptionEditModel : SubscriptionAddModel
	{
		[JsonRequired]
		public required int Id { get; init; }
	}
}
