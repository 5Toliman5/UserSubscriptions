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
		private const int MostUsedIndicatorsLimit = 3;
		private readonly IUserApiClient _userApiClient;
		private readonly IProjectRepository _projectRepository;

		public MetricsService(IUserApiClient userApiClient, IProjectRepository projectRepository)
		{
			_userApiClient = userApiClient;
			_projectRepository = projectRepository;
		}

		public async Task<Result<MostUsedIndicatorsModel>> GetMostUsedIndicatorsBySubscription(string subscriptionType)
		{
			var getUsersResult = await _userApiClient.GetUserIdsBySubscriptionType("Super");
			if (!getUsersResult.Successful)
				return Result<MostUsedIndicatorsModel>.Failure(getUsersResult);

			var userIds = getUsersResult.Data;
			if (userIds.IsNullOrEmpty())
				return Result<MostUsedIndicatorsModel>.Failure(ResultErrorType.NotFound, "No users found with the specified subscription type.");

			return Result<MostUsedIndicatorsModel>.Success(await _projectRepository.GetMostUsedIndicatorsBySubscription(userIds, MostUsedIndicatorsLimit));
		}
	}
}
