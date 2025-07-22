using AwesomeProject.Projects.Application.Repositories;
using AwesomeProject.Projects.Domain.Entities;
using MongoDB.Driver;

namespace AwesomeProject.Projects.Infrastructure.Repositories
{
	public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IDomainEntity
	{
		protected readonly IMongoCollection<TEntity> Collection;

		protected abstract string CollectionName { get; }

		public RepositoryBase(IMongoDatabase database)
		{
			Collection = database.GetCollection<TEntity>(CollectionName);
		}

		public Task<TEntity> GetByIdAsync(string id)
		{
			return Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
		}

		public Task CreateAsync(TEntity project)
		{
			return Collection.InsertOneAsync(project);
		}

		public Task UpdateAsync(TEntity project)
		{
			return Collection.ReplaceOneAsync(x => x.Id == project.Id, project);
		}

		public Task DeleteAsync(string id)
		{
			return Collection.DeleteOneAsync(x => x.Id == id);
		}
	}
}
