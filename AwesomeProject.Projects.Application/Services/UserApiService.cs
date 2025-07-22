using AwesomeProject.Common.Result;
using AwesomeProject.Projects.Application.ExternalServices;

namespace AwesomeProject.Projects.Application.Services
{
	public interface IUserApiService
	{
		Task<Result> CheckIfUserExists(int userId);
	}

	public class UserApiService : IUserApiService
	{
		private readonly IUserApiClient _userApiClient;

		public UserApiService(IUserApiClient userApiClient)
		{
			_userApiClient = userApiClient;
		}

		public async Task<Result> CheckIfUserExists(int userId)
		{
			var user = await _userApiClient.GetUserByIdAsync(userId);
			return user is not null
				? Result.Success()
				: Result.Failure(ResultErrorType.NotFound, $"User with ID {userId} does not exist.");
		}
	}
}
