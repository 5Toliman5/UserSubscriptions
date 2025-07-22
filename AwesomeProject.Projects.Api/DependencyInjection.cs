using AwesomeProject.Projects.Application.ExternalServices;
using AwesomeProject.Projects.Application.Repositories;
using AwesomeProject.Projects.Application.Services;
using AwesomeProject.Projects.Infrastructure;
using AwesomeProject.Projects.Infrastructure.ExternalServices;
using AwesomeProject.Projects.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AwesomeProject.Users.Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection RegisterDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
		{
			var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
			services.AddSingleton<IMongoClient>(sp =>
			{

				return new MongoClient(settings.ConnectionString);
			});

			services.AddScoped(sp =>
			{
				var client = sp.GetRequiredService<IMongoClient>();
				return client.GetDatabase(settings.DatabaseName);
			});
			services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
			services.AddScoped<IProjectRepository, ProjectRepository>();
			services.AddScoped<IUserApiService, UserApiService>();
			services.AddScoped<IMetricsService, MetricsService>();
			return services;
		}
		public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHttpClient<IUserApiClient, UserApiClient>(client =>
			{
				client.BaseAddress = new Uri(configuration["UserApiUrl"]);
			});
			services.AddScoped<IUserSettingsService, UserSettingsService>();
			services.AddScoped<IProjectService, ProjectService>();
			return services;
		}
	}
}
