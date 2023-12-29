using Exa.Configure.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Core.MongoDB
{
    public interface IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Table { get; }

        Task<TEntity> GetById(string id);

        Task<List<TEntity>> GetAll();

        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> filter);

        Task<long> GetCount(Expression<Func<TEntity, bool>> filter);

        Task<List<TEntity>> GetListExtended(Expression<Func<TEntity, bool>> filter, int limit, int skip);

        bool Exists(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> Save(TEntity entity);

        Task<ReplaceOneResult> Update(TEntity entity);

        Task<UpdateResult> UpdateField<U>(string id, Expression<Func<TEntity, U>> expression, U value);

        Task<UpdateResult> UpdateOne(Expression<Func<TEntity, bool>> filterexpression, UpdateBuilder<TEntity> updateBuilder);

        Task<UpdateResult> UpdateMany(Expression<Func<TEntity, bool>> filterexpression, UpdateBuilder<TEntity> updateBuilder);

        Task<DeleteResult> Remove(string id);
    }
}
