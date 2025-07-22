using AwesomeProject.Common.Result;
using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Repositories;
using AwesomeProject.Projects.Domain.Entities;

namespace AwesomeProject.Projects.Application.Services
{
	public interface IUserSettingsService
	{
		Task<Result> CreateAsync(UserSettingsModel model);
		Task<UserSettingsViewModel?> GetByUserIdAsync(int userId);
		Task UpdateAsync(UserSettingsEditModel model);
	}

	public class UserSettingsService : IUserSettingsService
	{
		private readonly IUserSettingsRepository _repository;

		public UserSettingsService(IUserSettingsRepository repository)
		{
			_repository = repository;
		}

		public async Task<UserSettingsViewModel?> GetByUserIdAsync(int userId)
		{
			var settings = await _repository.GetByUserIdAsync(userId);
			return MapToView(settings);
		}

		public async Task<Result> CreateAsync(UserSettingsModel model)
		{
			if (await GetByUserIdAsync(model.UserId) is not null)
				return Result.Failure(ResultErrorType.Conflict, $"User settings for user ID {model.UserId} already exist.");

			await _repository.CreateAsync(MapToEntity(model));
			return Result.Success();
		}

		public Task UpdateAsync(UserSettingsEditModel model)
		{
			return _repository.UpdateAsync(MapToEntity(model));
		}

		private static UserSettings MapToEntity(UserSettingsModel model)
		{
			return new UserSettings
			{
				UserId = model.UserId,
				Language = model.Language,
				Theme = model.Theme
			};
		}

		private static UserSettingsViewModel? MapToView(UserSettings entity)
		{
			return entity is null
				? null
				: new UserSettingsViewModel
				{
					Id = entity.Id,
					UserId = entity.UserId,
					Language = entity.Language,
					Theme = entity.Theme
				};
		}
	}
}
