using AwesomeProject.Common.Result;
using AwesomeProject.Common.Utilities;
using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Domain.Entities;

namespace AwesomeProject.Users.Application.Services
{
	public interface IUserSubscriptionService
	{
		Task<Result<int[]>> GetUserIdsBySubscriptionType(string subscriptionType);
		Task<Result> SubscribeUserAsync(int userId, int subscriptionId);


	}

	public class UserSubscriptionService : IUserSubscriptionService
	{
		private readonly IUserRepository _userRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;

		public UserSubscriptionService(IUserRepository userRepository, ISubscriptionRepository subscriptionRepository)
		{
			_userRepository = userRepository;
			_subscriptionRepository = subscriptionRepository;
		}

		public async Task<Result> SubscribeUserAsync(int userId, int subscriptionId)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			if (user == null)
			{
				return Result.Failure(ResultErrorType.NotFound, "User not found.");
			}
			var subscription = await _subscriptionRepository.GetByIdAsync(subscriptionId);
			if (subscription == null)
			{
				return Result.Failure(ResultErrorType.NotFound, "Subscription not found.");
			}
			user.Subscription = subscription;
			await _userRepository.SaveChangesAsync();
			return Result.Success();
		}

		public async Task<Result<int[]>> GetUserIdsBySubscriptionType(string subscriptionType)
		{
			if (!EnumAttributeUtility.TryGetValueFromDescription<SubscriptionType>(subscriptionType, out var type))
				return Result<int[]>.Failure(ResultErrorType.Validation, $"{subscriptionType} is not a valid SubscriptionType");

			var result = await _userRepository.GetUserIdsBySubscriptionType(type);
			return Result<int[]>.Success(result);
		}
	}
}
