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

		public async Task<MostUsedIndicatorsModel> GetMostUsedIndicatorsBySubscription(int[] userIds, int limit)
		{
			var pipeline = new BsonDocument[]
			{
				new BsonDocument("$match", new BsonDocument("userId", new BsonDocument("$in", new BsonArray(userIds)))),
				new BsonDocument("$unwind", "$charts"),
				new BsonDocument("$unwind", "$charts.indicators"),
				new BsonDocument("$group", new BsonDocument
				{
					{ "_id", "$charts.indicators.name" },
					{ "count", new BsonDocument("$sum", 1) }
				}),
				new BsonDocument("$sort", new BsonDocument("count", -1)),
				new BsonDocument("$limit", limit)
			};

			var result = await Collection.Aggregate<BsonDocument>(pipeline).ToListAsync();

			var mapped = result.Select(d => new MostUsedIndicator
			{
				Name = d["_id"].AsString,
				Used = d["count"].AsInt32
			}).ToList();

			return new MostUsedIndicatorsModel { Indicators = mapped };
		}
	}
}
