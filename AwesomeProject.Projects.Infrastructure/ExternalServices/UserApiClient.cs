using AwesomeProject.Common.Result;
using AwesomeProject.Projects.Application.ExternalServices;
using AwesomeProject.Projects.Application.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace AwesomeProject.Projects.Infrastructure.ExternalServices
{
	public class UserApiClient : IUserApiClient
	{
		private readonly HttpClient _httpClient;

		public UserApiClient(HttpClient httpClient, ILogger<UserApiClient> logger)
		{
			_httpClient = httpClient;
		}

		public async Task<UserModel?> GetUserByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"Users/{id}");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<UserModel>();
		}

		public async Task<Result<int[]?>> GetUserIdsBySubscriptionType(string subscriptionType)
		{
			var response = await _httpClient.GetAsync($"UserSubscription/getUsersBySubscriptionType?subscriptionType={subscriptionType}");
			if (!response.IsSuccessStatusCode)
			{
				return Result<int[]?>.Failure( ResultErrorType.Conflict,
					$"Failed to get user IDs by subscription type '{subscriptionType}'. " +
					$"Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}"
				);
			}
			return Result<int[]?>.Success(await response.Content.ReadFromJsonAsync<int[]>());
		}
	}
}
