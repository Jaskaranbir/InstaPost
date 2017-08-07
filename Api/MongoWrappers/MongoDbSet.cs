using Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Api.MongoWrappers {
    public class MongoDbSet<TEntity> : IMongoDbSet<TEntity> where TEntity : class
    {
        private readonly IMongoQueryable<TEntity> queryCollection;
        private readonly IMongoCollection<TEntity> mongoCollection;

        /// <summary>
        ///     Creates one MongoDbSet for storing MongoDb collection
        /// </summary>
        public MongoDbSet(IMongoDatabase mongodb) {
            string collectionName = typeof(TEntity).Name.ToLower();
            IMongoCollection<TEntity> collection = mongodb.GetCollection<TEntity>(collectionName);

            this.mongoCollection = collection;
            this.queryCollection = collection.AsQueryable<TEntity>();
        }

        /// <summary>
        ///     Used to insert one entity in database.
        ///     This method is a wrapper for IMongoCollection.InsertOne
        /// </summary>
        public TEntity Add(TEntity entity) {
            mongoCollection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        ///     Used to asynchronously insert one entity in database.
        ///     This method is a wrapper for IMongoCollection.InsertOneAsync
        /// </summary>
        public TEntity AddAsync(TEntity entity) {
            mongoCollection.InsertOneAsync(entity);
            return entity;
        }

        /// <summary>
        ///     Used to insert an array of entities in database.
        ///     This method is a wrapper for IMongoCollection.AddRange
        /// </summary>
        public TEntity[] AddRange(params TEntity[] entities) {
            entities.Select(b => b.ToBsonDocument());
            mongoCollection.InsertMany(entities);
            return entities;
        }

        /// <summary>
        ///     Used to asynchronously insert an array of entities in database.
        ///     This method is a wrapper for IMongoCollection.AddRangeAsync
        /// </summary>
        public TEntity[] AddRangeAsync(params TEntity[] entities) {
            entities.Select(b => b.ToBsonDocument());
            mongoCollection.InsertManyAsync(entities);
            return entities;
        }

        /// <summary>
        ///     Used to update a record in database. This replaces entire matched document.
        ///     Consider using UpdateOne instead.
        ///     This method is a wrapper for IMongoCollection.FindOneAndReplace
        /// </summary>
        public TEntity Update(TEntity updatedEntity, Expression<Func<TEntity, bool>> filter, FindOneAndReplaceOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
            TEntity oldEntity = mongoCollection.FindOneAndReplace<TEntity>(
                filter,
                updatedEntity,
                options,
                cancellationToken
            );

            return oldEntity;
        }

        /// <summary>
        ///     Used to asynchronously update a record in database. This replaces entire matched document.
        ///     Consider using UpdateOneAsync instead.
        ///     This method is a wrapper for IMongoCollection.FindOneAndReplaceAsync
        /// </summary>
        public async Task<TEntity> UpdateAsync(TEntity updatedEntity, Expression<Func<TEntity, bool>> filter, FindOneAndReplaceOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
            TEntity oldEntity = await mongoCollection.FindOneAndReplaceAsync<TEntity>(
                filter,
                updatedEntity,
                options,
                cancellationToken
            );

            return oldEntity;
        }

        /// <summary>
        ///     Update document with custom filter and update.
        ///     This method is a wrapper for IMongoCollection.UpdateOne
        /// </summary>
        public TEntity UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
            return mongoCollection.FindOneAndUpdate<TEntity>(filter, update, options, cancellationToken);
        }

        /// <summary>
        ///     Update document asynchronously with custom filter and update.
        ///     This method is a wrapper for IMongoCollection.UpdateOnAsync
        /// </summary>
        public async Task<TEntity> UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
            return await mongoCollection.FindOneAndUpdateAsync<TEntity>(filter, update, options, cancellationToken);
        }

        /// <summary>
        ///     Used to delete a record in database.
        ///     This method is a wrapper for IMongoCollection.FindOneAndDelete
        /// </summary>
        public TEntity Remove(Expression<Func<TEntity, bool>> filter, FindOneAndDeleteOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
            TEntity entity = mongoCollection.FindOneAndDelete<TEntity>(
                filter,
                options,
                cancellationToken
            );
            return entity;
        }

        /// <summary>
        ///     Used to asynchronously delete a record in database.
        ///     This method is a wrapper for IMongoCollection.FindOneAndDeleteAsync
        /// </summary>
        public async Task<TEntity> RemoveAsync(Expression<Func<TEntity, bool>> filter, FindOneAndDeleteOptions<TEntity> options = null, CancellationToken cancellationToken = default(CancellationToken)) {
            TEntity entity = await mongoCollection.FindOneAndDeleteAsync<TEntity>(
                filter,
                options,
                cancellationToken
            );
            return entity;
        }

        /// <summary>
        ///     Returns original IMongoCollection for custom invocations.
        /// </summary>
        public IMongoCollection<TEntity> GetBaseCollection() {
            return mongoCollection;
        }

        /// <summary>
        ///     Begins a fluent find interface.
        /// </summary>
        public IFindFluent<TEntity, TEntity> Find(Expression<Func<TEntity, bool>> filter, FindOptions options = null) {
            return mongoCollection.Find(filter, options);
        }

        public IFindFluent<TEntity, TEntity> Find(FilterDefinition<TEntity> filter, FindOptions options = null) {
            return mongoCollection.Find(filter, options);
        }

        public IAggregateFluent<TEntity> Aggregate(AggregateOptions options = null) {
            return mongoCollection.Aggregate(options);
        }

        public bool CheckEntity(FilterDefinition<TEntity> filter) {
            TEntity e = this.Find(filter).FirstOrDefault();
            return e != null;
        }

        public TEntity CheckAndCreateEntity(TEntity entity, FilterDefinition<TEntity> checkFilter) {
            if(!CheckEntity(checkFilter))
                this.Add(entity);

            return this.Find(checkFilter).FirstOrDefault();
        }

        public bool CheckAndCreateEntityBool(TEntity entity, FilterDefinition<TEntity> checkFilter) {
            if (!CheckEntity(checkFilter)) {
                this.Add(entity);
                return false;
            }
            return true;
        }

        public Type ElementType => queryCollection.GetType();

        public Expression Expression => queryCollection.Expression;

        public IQueryProvider Provider => queryCollection.Provider;

        public IEnumerator<TEntity> GetEnumerator() {
            return queryCollection.GetEnumerator();
        }

        public QueryableExecutionModel GetExecutionModel() {
            return queryCollection.GetExecutionModel();
        }

        public IAsyncCursor<TEntity> ToCursor(CancellationToken cancellationToken = default(CancellationToken)) {
            return queryCollection.ToCursor(cancellationToken);
        }

        public Task<IAsyncCursor<TEntity>> ToCursorAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return queryCollection.ToCursorAsync(cancellationToken);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return queryCollection.GetEnumerator();
        }
    }
}
