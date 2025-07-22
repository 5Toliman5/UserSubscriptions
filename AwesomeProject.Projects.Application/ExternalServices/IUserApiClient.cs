using AwesomeProject.Common.Result;
using AwesomeProject.Projects.Application.Models;

namespace AwesomeProject.Projects.Application.ExternalServices
{
	public interface IUserApiClient
	{
		Task<UserModel?> GetUserByIdAsync(int id);
		Task<Result<int[]?>> GetUserIdsBySubscriptionType(string subscriptionType);
	}
}
