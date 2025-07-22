using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel;

namespace AwesomeProject.Projects.Domain.Entities
{
	public class Project : IDomainEntity
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public int UserId { get; set; }

		public string Name { get; set; }

		public List<Chart> Charts { get; set; }
	}

	public class Chart
	{
		public string Symbol { get; set; }

		public string Timeframe { get; set; }

		public List<Indicator> Indicators { get; set; }
	}

	public class Indicator
	{
		public string Name { get; set; }

		public string Parameters { get; set; }
	}

	public enum Symbol
	{
		[Description("EURUSD")]
		EURUSD = 1,

		[Description("USDJPY")]
		USDJPY = 2,
	}

	public enum Timeframe
	{
		[Description("M1")]
		M1 = 1,

		[Description("M5")]
		M5 = 2,

		[Description("H1")]
		H1 = 2,
	}

	public enum IndicatorName
	{
		[Description("MA")]
		MA = 1,

		[Description("BB")]
		BB = 2,

		[Description("RSI")]
		RSI = 2,

		[Description("Ichimoku")]
		Ichimoku = 2,
	}
}
