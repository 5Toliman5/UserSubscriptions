﻿namespace AwesomeProject.Users.Application.Models
{
	public record UserViewModel(int Id, string Name, string Email, UserSubscriptionModel? Subscription = null);

	public record UserSubscriptionModel(string Type, DateTime StartDate, DateTime EndDate);
}
