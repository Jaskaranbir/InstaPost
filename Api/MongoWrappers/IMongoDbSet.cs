using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Api.MongoWrappers {
    public interface IMongoDbSet<TEntity> : IMongoQueryable<TEntity> where TEntity : class {

        TEntity Add(TEntity entity);
        TEntity AddAsync(TEntity entity);
        TEntity[] AddRange(params TEntity[] entities);
        TEntity[] AddRangeAsync(params TEntity[] entities);

        bool CheckEntity(FilterDefinition<TEntity> filter);
        TEntity CheckAndCreateEntity(TEntity entity, FilterDefinition<TEntity> checkFilter);
        bool CheckAndCreateEntityBool(TEntity entity, FilterDefinition<TEntity> checkFilter);

        IFindFluent<TEntity, TEntity> Find(Expression<Func<TEntity, bool>> filter, FindOptions options = null);
        IFindFluent<TEntity, TEntity> Find(FilterDefinition<TEntity> filter, FindOptions options = null);
        IAggregateFluent<TEntity> Aggregate(AggregateOptions options = null);
        IMongoCollection<TEntity> GetBaseCollection();

        TEntity Remove(Expression<Func<TEntity, bool>> filter, FindOneAndDeleteOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> RemoveAsync(Expression<Func<TEntity, bool>> filter, FindOneAndDeleteOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken));

        TEntity Update(TEntity updatedEntity, Expression<Func<TEntity, bool>> filter, FindOneAndReplaceOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> UpdateAsync(TEntity updatedEntity, Expression<Func<TEntity, bool>> filter, FindOneAndReplaceOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken));

        TEntity UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}