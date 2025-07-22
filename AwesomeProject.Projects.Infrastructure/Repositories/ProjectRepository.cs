using AwesomeProject.Projects.Application.Models;
using AwesomeProject.Projects.Application.Repositories;
using AwesomeProject.Projects.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AwesomeProject.Projects.Infrastructure.Repositories
{
	public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
	{
		protected override string CollectionName => "projects";
		public ProjectRepository(IMongoDatabase database) : base(database)
		{
		}

		public Task<List<Project>> GetByUserIdAsync(int userId)
		{
			return Collection.Find(x => x.UserId == userId).ToListAsync();
		}

		public Task<List<Project>> GetByUserIdsAsync(int[] userIds)
		{
			return Collection.Find(x => userIds.Contains( x.UserId)).ToListAsync();
		}
	}
}
