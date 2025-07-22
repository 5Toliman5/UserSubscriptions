namespace AwesomeProject.Projects.Application.Models
{
	public class MostUsedIndicatorsModel
	{
		public List<MostUsedIndicator> Indicators { get; set; }
	}

	public class  MostUsedIndicator
	{
		public string Name { get; set; }

		public int Used { get; set; }
	}
}
