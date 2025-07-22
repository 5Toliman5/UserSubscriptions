using AwesomeProject.Common.Extensions;
using AwesomeProject.Common.Result;
using AwesomeProject.Users.Application.Models;
using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Domain.Entities;

namespace AwesomeProject.Users.Application.Services
{
	public interface IUserService
	{
		Task<UserViewModel> AddAsync(UserAddModel model);
		Task<Result> DeleteAsync(int id);
		Task<List<UserViewModel>> GetAllAsync();
		Task<Result<UserViewModel>> GetByIdAsync(int id);
		Task<Result<UserViewModel>> UpdateAsync(UserEditModel model);
	}

	public class UserService(IUserRepository repository) : IUserService
	{
		private readonly IUserRepository _repository = repository;

		public async Task<List<UserViewModel>> GetAllAsync()
		{
			var users = await _repository.GetAllAsync();

			return users.IsNullOrEmpty()
				? []
				: users.Select(user => new UserViewModel(
				user.Id,
				user.Name,
				user.Email,
				user.Subscription is null ? null : new UserSubscriptionModel(
					user.Subscription.Type.GetDescription(),
					user.Subscription.StartDate,
					user.Subscription.EndDate
				)
			)).ToList();
		}

		public async Task<Result<UserViewModel>> GetByIdAsync(int id)
		{
			var user = await _repository.GetByIdAsync(id);
			return user is null
				? Result<UserViewModel>.Failure(ResultErrorType.NotFound)
				: Result<UserViewModel>.Success();
		}

		public async Task<Result<UserViewModel>> UpdateAsync(UserEditModel model)
		{
			var user = await _repository.GetByIdAsync(model.Id);
			if (user is null)
			{
				return Result<UserViewModel>.Failure(ResultErrorType.NotFound);
			}

			user.Name = model.Name;
			user.Email = model.Email;

			await _repository.SaveChangesAsync();
			return Result<UserViewModel>.Success(new UserViewModel(user.Id, user.Name, user.Email));
		}

		public async Task<UserViewModel> AddAsync(UserAddModel model)
		{
			var user = new User(model.Name, model.Email);

			_repository.Add(user);
			await _repository.SaveChangesAsync();

			return new UserViewModel(user.Id, user.Name, user.Email);
		}

		public async Task<Result> DeleteAsync(int id)
		{
			var result = await _repository.DeleteByIdAsync(id);
			if (!result.Successful)
			{
				return result;
			}
			await _repository.SaveChangesAsync();
			return Result.Success();
		}
	}
}
