
using AwesomeProject.Common.Middleware;
using AwesomeProject.Users.Api;

namespace AwesomeProject.Projects.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.RegisterDataAccessLayer(builder.Configuration);
			builder.Services.RegisterApplicationLayer(builder.Configuration);

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader();
				});
			});
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			//if (app.Environment.IsDevelopment())
			//{
				app.UseSwagger();
				app.UseSwaggerUI();
			//}

			//app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();
			app.UseMiddleware<ErrorHandlerMiddleware>();
			app.Run();
		}
	}
}
