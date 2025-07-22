using AwesomeProject.Common.Extensions;
using AwesomeProject.Common.Result;
using AwesomeProject.Common.Utilities;
using AwesomeProject.Users.Application.Models;
using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Domain.Entities;

namespace AwesomeProject.Users.Application.Services
{
	public interface ISubscriptionService
	{
		Task<Result<SubscriptionViewModel>> AddAsync(SubscriptionAddModel model);
		Task<Result> DeleteAsync(int id);
		Task<List<SubscriptionViewModel>> GetAllAsync();
		Task<Result<SubscriptionViewModel>> GetByIdAsync(int id);
		Task<Result<SubscriptionViewModel>> UpdateAsync(SubscriptionEditModel model);
	}

	public class SubscriptionService(ISubscriptionRepository repository) : ISubscriptionService
	{
		private readonly ISubscriptionRepository _repository = repository;

		public async Task<List<SubscriptionViewModel>> GetAllAsync()
		{
			var subscriptions = await _repository.GetAllAsync();

			return subscriptions.IsNullOrEmpty()
				? []
				: subscriptions.Select(Subscription => new SubscriptionViewModel(
				Subscription.Id,
				Subscription.Type.GetDescription(),
				Subscription.StartDate,
				Subscription.EndDate
			)).ToList();
		}

		public async Task<Result<SubscriptionViewModel>> GetByIdAsync(int id)
		{
			var user = await _repository.GetByIdAsync(id);

			return user is null
				? Result<SubscriptionViewModel>.Failure(ResultErrorType.NotFound)
				: Result<SubscriptionViewModel>.Success();
		}

		public async Task<Result<SubscriptionViewModel>> UpdateAsync(SubscriptionEditModel model)
		{
			var subscription = await _repository.GetByIdAsync(model.Id);

			if (subscription is null)
				return Result<SubscriptionViewModel>.Failure(ResultErrorType.NotFound);

			if (!EnumAttributeUtility.TryGetValueFromDescription<SubscriptionType>(model.Type, out var type))
				return Result<SubscriptionViewModel>.Failure(ResultErrorType.Validation, $"{model.Type} is not a valid SubscriptionType");

			subscription.Type = type;
			subscription.StartDate = model.StartDate;
			subscription.EndDate = model.EndDate;

			await _repository.SaveChangesAsync();
			return Result<SubscriptionViewModel>.Success(new SubscriptionViewModel(
				subscription.Id, 
				subscription.Type.GetDescription(), 
				subscription.StartDate, 
				subscription.EndDate
				));
		}

		public async Task<Result<SubscriptionViewModel>> AddAsync(SubscriptionAddModel model)
		{
			if (!EnumAttributeUtility.TryGetValueFromDescription<SubscriptionType>(model.Type, out var type))
				return Result<SubscriptionViewModel>.Failure(ResultErrorType.Validation, $"{model.Type} is not a valid SubscriptionType");

			var subscription = new Subscription(type, model.StartDate, model.EndDate);

			_repository.Add(subscription);
			await _repository.SaveChangesAsync();

			return Result<SubscriptionViewModel>.Success(new SubscriptionViewModel(subscription.Id, subscription.Type.GetDescription(), subscription.StartDate, subscription.EndDate));
		}

		public async Task<Result> DeleteAsync(int id)
		{
			var result = await _repository.DeleteByIdAsync(id);

			if (!result.Successful)
				return result;

			await _repository.SaveChangesAsync();

			return Result.Success();
		}
	}
}
