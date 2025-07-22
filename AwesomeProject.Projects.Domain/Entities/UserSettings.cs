using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace AwesomeProject.Projects.Domain.Entities
{
	public class UserSettings : IDomainEntity
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; private set; }

		public int UserId { get; set; }

		public string Language { get; set; }

		public string Theme { get; set; }
	}

	public enum SettingsLanguage
	{
		[Description("English")]
		English = 1,

		[Description("Spanish")]
		Spanish = 2,

		[Description("French")]
		French = 3,

		[Description("German")]
		German = 4
	}

	public enum  SettingsTheme
	{
		[Description("light")]
		Light = 1,

		[Description("dark")]
		Dark = 2,
	}
}
