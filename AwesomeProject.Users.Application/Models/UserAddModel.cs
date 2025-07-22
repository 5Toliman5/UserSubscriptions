using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AwesomeProject.Users.Application.Models
{
	public record UserAddModel
	{
		[JsonRequired, MaxLength(64)]
		public required string Name { get; init; }

		[JsonRequired, EmailAddress, MaxLength(64)]
		public required string Email { get; init; }
	}
}
