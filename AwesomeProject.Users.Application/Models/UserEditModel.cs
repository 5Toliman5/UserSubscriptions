using System.Text.Json.Serialization;

namespace AwesomeProject.Users.Application.Models
{

	public record UserEditModel : UserAddModel
	{
		[JsonRequired]
		public required int Id { get; init; }
	}
}
