using AwesomeProject.Users.Application.Repositories;
using AwesomeProject.Users.Application.Services;
using AwesomeProject.Users.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AwesomeProject.Users.Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection RegisterDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
			return services;
		}
		public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ISubscriptionService, SubscriptionService>();
			services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
			services.AddScoped<IUserService, UserService>();
			return services;
		}
	}
}
