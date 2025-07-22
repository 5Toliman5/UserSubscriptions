using AwesomeProject.Common.Extensions;
using AwesomeProject.Common.Utilities;
using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Repositories;
using AwesomeProject.Projects.Domain.Entities;

namespace AwesomeProject.Projects.Application.Services
{
	public interface IProjectService
	{
		Task CreateAsync(ProjectModel model);
		Task DeleteAsync(string id);
		Task<ProjectViewModel?> GetByIdAsync(string id);
		Task<List<ProjectViewModel>> GetByUserIdAsync(int userId);
		Task UpdateAsync(ProjectEditModel model);
	}

	public class ProjectService : IProjectService
	{
		private readonly IProjectRepository _repository;

		public ProjectService(IProjectRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<ProjectViewModel>> GetByUserIdAsync(int userId)
		{
			var projects = await _repository.GetByUserIdAsync(userId);
			return projects.Select(MapToDto).ToList();
		}

		public async Task<ProjectViewModel?> GetByIdAsync(string id)
		{
			var project = await _repository.GetByIdAsync(id);
			return project is null
				? null
				: MapToDto(project);
		}

		public Task CreateAsync(ProjectModel model)
		{
			return _repository.CreateAsync(MapToEntity(model));
		}

		public Task UpdateAsync(ProjectEditModel model)
		{
			return _repository.UpdateAsync(MapToEntity(model));
		}

		public Task DeleteAsync(string id)
		{
			return _repository.DeleteAsync(id);
		}

		private static ProjectViewModel MapToDto(Project project)
		{
			return new()
			{
				Id = project.Id,
				UserId = project.UserId,
				Name = project.Name,
				Charts = project.Charts.Select(c => new ChartModel
				{
					Symbol = EnumAttributeUtility.GetValueFromDescription<Symbol>(c.Symbol),
					Timeframe = EnumAttributeUtility.GetValueFromDescription<Timeframe>(c.Timeframe),
					Indicators = c.Indicators.Select(i => new IndicatorModel
					{
						Name = EnumAttributeUtility.GetValueFromDescription<IndicatorName>(i.Name),
						Parameters = i.Parameters
					}).ToList()
				}).ToList()
			};
		}

		private static Project MapToEntity(ProjectModel model)
		{
			var project = new Project()
			{
				UserId = model.UserId,
				Name = model.Name,
				Charts = model.Charts.Select(c => new Chart
				{
					Symbol = c.Symbol.GetDescription(),
					Timeframe = c.Timeframe.GetDescription(),
					Indicators = c.Indicators.Select(i => new Indicator
					{
						Name = i.Name.GetDescription(),
						Parameters = i.Parameters
					}).ToList()
				}).ToList()
			};
			if (model is ProjectEditModel editModel)
			{
				project.Id = editModel.Id;
			}
			return project;
		}
	}
}
