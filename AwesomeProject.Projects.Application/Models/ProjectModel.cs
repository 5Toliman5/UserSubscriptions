using AwesomeProject.Projects.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AwesomeProject.Projects.Application.Models
{
	public record ProjectModel
	{
		[JsonRequired]
		public int UserId { get; set; }

		[JsonRequired, MinLength(2), MaxLength(255)]
		public string Name { get; set; }

		[JsonRequired]
		public List<ChartModel> Charts { get; set; } = [];
	}

	public record ProjectEditModel : ProjectModel
	{
		[JsonRequired]
		public string Id { get; set; }

		[JsonRequired, MinLength(2), MaxLength(255)]
		public string Name { get; set; }

		[JsonRequired]
		public List<ChartModel> Charts { get; set; } = [];
	}

	public record ProjectViewModel : ProjectModel
	{
		[JsonRequired]
		public string Id { get; set; }
	}

	public class ChartModel
	{
		[JsonRequired]
		public Symbol Symbol { get; set; }

		[JsonRequired]
		public Timeframe Timeframe { get; set; }

		[JsonRequired]
		public List<IndicatorModel> Indicators { get; set; } = [];
	}

	public class IndicatorModel
	{
		[JsonRequired]
		public IndicatorName Name { get; set; }

		[JsonRequired, MinLength(2)]
		public string Parameters { get; set; }
	}
}
