using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AwesomeProject.Projects.Application.Models
{
	public record UserSettingsModel
	{
		[JsonRequired]
		public int UserId { get; set; }

		[JsonRequired, MinLength(2), MaxLength(20)]
		public string Language { get; set; }

		[JsonRequired, MinLength(1), MaxLength(20)]
		public string Theme { get; set; }
	}

	public record UserSettingsEditModel : UserSettingsModel
	{
		[JsonRequired]
		public string Id { get; set; }
	}

	public record UserSettingsViewModel : UserSettingsEditModel;

}
