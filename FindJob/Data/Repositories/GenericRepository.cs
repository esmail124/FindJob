using FindJob.Data.Entities;
using FindJob.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FindJob.Data.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(ObjectId id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(ObjectId id, TEntity entity);
        Task RemoveAsync(ObjectId id);
    }

    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            var entities = await _collection.Find(_ => true).ToListAsync();
            return entities;
        }

        public async Task<TEntity?> GetByIdAsync(ObjectId id)
        {
            var entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(ObjectId id, TEntity updatedEntity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedEntity);
        }

        public async Task RemoveAsync(ObjectId id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }


    
}
