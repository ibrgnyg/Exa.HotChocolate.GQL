using Exa.Configure.Models;
using Exa.Configure.Models.Configure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Exa.Core.MongoDB
{
    public partial class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        protected IMongoCollection<TEntity>? _collection;

        public IMongoCollection<TEntity>? Collection { get { return _collection; } }

        protected IMongoDatabase? _database;

        public IMongoDatabase? Database { get { return _database; } }

        private readonly IOptions<DBSettings> _settings;

        public MongoRepository(IOptions<DBSettings> settings)
        {
            _settings = settings;
            var client = new MongoClient(_settings.Value.ConnectionString);
            _database = client.GetDatabase(_settings.Value.DatabaseName);
            _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public IQueryable<TEntity> Table => _collection.AsQueryable();

        public virtual Task<TEntity> GetById(string id)
        {
            return _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return _collection.AsQueryable().ToListAsync();
        }

        public virtual Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual Task<long> GetCount(Expression<Func<TEntity, bool>> filter)
        {
            return _collection.Find(filter).CountDocumentsAsync();
        }

        public virtual Task<List<TEntity>> GetListExtended(Expression<Func<TEntity, bool>> filter, int limit, int skip)
        {
            return _collection.Find(filter).Limit(limit).Skip(skip).ToListAsync();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return GetCount(filter).GetAwaiter().GetResult() > 0;
        }

        public virtual async Task<TEntity> Save(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<ReplaceOneResult> Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            return await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true });
        }

        public virtual async Task<UpdateResult> UpdateField<U>(string id, Expression<Func<TEntity, U>> expression, U value)
        {
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Eq(x => x.Id, id);
            var update = Builders<TEntity>.Update
                .Set(expression, value)
                .Set(x => x.UpdateDate, DateTime.UtcNow);

            return await _collection.UpdateOneAsync(filter, update);
        }

        public virtual async Task<UpdateResult> UpdateOne(Expression<Func<TEntity, bool>> filterexpression, UpdateBuilder<TEntity> updateBuilder)
        {
            updateBuilder
               .Set(x => x.UpdateDate, DateTime.UtcNow);

            var update = Builders<TEntity>.Update.Combine(updateBuilder.Fields);
            return await _collection.UpdateOneAsync(filterexpression, update);
        }

        public async Task<UpdateResult> UpdateMany(Expression<Func<TEntity, bool>> filterexpression, UpdateBuilder<TEntity> updateBuilder)
        {
            updateBuilder
             .Set(x => x.UpdateDate, DateTime.UtcNow);

            var update = Builders<TEntity>.Update.Combine(updateBuilder.Fields);
            return await _collection?.UpdateManyAsync(filterexpression, update);
        }

        public virtual async Task<DeleteResult> Remove(string id)
        {
            return _collection.DeleteMany(x => x.Id == id);
        }
    }
}
