using AwesomeProject.Common.Extensions;
using AwesomeProject.Common.Result;
using AwesomeProject.Projects.Application.ExternalServices;
using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Repositories;

namespace AwesomeProject.Projects.Application.Services
{
	public interface IMetricsService
	{
		Task<Result<MostUsedIndicatorsModel>> GetMostUsedIndicatorsBySubscription(string subscriptionType);
	}

	public class MetricsService : IMetricsService
	{
		private readonly IUserApiClient _userApiClient;
		private readonly IProjectRepository _projectRepository;

		public MetricsService(IUserApiClient userApiClient, IProjectRepository projectRepository)
		{
			_userApiClient = userApiClient;
			_projectRepository = projectRepository;
		}

		public async Task<Result<MostUsedIndicatorsModel>> GetMostUsedIndicatorsBySubscription(string subscriptionType)
		{
			var getUsersResult = await _userApiClient.GetUserIdsBySubscriptionType(subscriptionType.ToLower());
			if (!getUsersResult.Successful)
				return Result<MostUsedIndicatorsModel>.Failure(getUsersResult);

			var userIds = getUsersResult.Data;
			if (userIds.IsNullOrEmpty())
				return Result<MostUsedIndicatorsModel>.Failure(ResultErrorType.NotFound, "No users found with the specified subscription type.");

			var projects = await _projectRepository.GetByUserIdsAsync(userIds);

			var top3Indicators = projects
				.SelectMany(p => p.Charts)
				.SelectMany(c => c.Indicators)
				.GroupBy(i => i.Name)
				.Select(g => new MostUsedIndicator { Name = g.Key, Used = g.Count() })
				.OrderByDescending(x => x.Used)
				.Take(Constants.MostUsedIndicatorsLimit)
				.ToList();

			return Result<MostUsedIndicatorsModel>.Success(new MostUsedIndicatorsModel() { Indicators = top3Indicators });
		}
	}
}
