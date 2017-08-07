using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.MongoWrappers {
    public class MongoArrayUtils<TEntity> where TEntity : class {

        public static Task<TEntity> AddToArray<ArrDataType>(
            IMongoDbSet<TEntity> entity,
            FilterDefinition<TEntity> filter,
            FieldDefinition<TEntity> dbArrayFieldName,
            ArrDataType[] insertValues
        ) {
            UpdateDefinition<TEntity> update = Builders<TEntity>.Update.AddToSetEach(dbArrayFieldName, insertValues);

            Task<TEntity> e = entity.UpdateOneAsync(filter, update);
            return e;
        }

        public static Task<TEntity> AddToArrayWithCount<ArrDataType>(
            IMongoDbSet<TEntity> entity,
            FilterDefinition<TEntity> filter,
            FieldDefinition<TEntity> dbArrayFieldName,
            ArrDataType[] insertValues,
            ArrDataType[] initialValues,
            FieldDefinition<TEntity, int> countFieldDefinition
        ) {
            bool isIncCountEnabled = countFieldDefinition.ToString() != "";

            // If countFieldDefinition is blank, we dont enable count
            List<ArrDataType> uniqueInserts = new List<ArrDataType>();

            foreach (ArrDataType iVal in insertValues)
                if (Array.IndexOf(initialValues, iVal) == -1)
                    uniqueInserts.Add(iVal);

            int count = uniqueInserts.Count();
            if (count > 0) {
                UpdateDefinition<TEntity> update = Builders<TEntity>.Update
                    .PushEach(dbArrayFieldName, uniqueInserts)
                    .Inc(countFieldDefinition, uniqueInserts.Count());

                return entity.UpdateOneAsync(filter, update);
            }
            return null;
        }

        public static Task<TEntity> RemoveFromArray<ArrDataType>(
            IMongoDbSet<TEntity> entity,
            FilterDefinition<TEntity> filter,
            FieldDefinition<TEntity> dbArrayFieldName,
            ArrDataType[] fieldValue
        ) {
            UpdateDefinition<TEntity> update = Builders<TEntity>.Update.PullAll(
                dbArrayFieldName,
                fieldValue
            );

            return entity.UpdateOneAsync(filter, update);
        }

        public static Task<TEntity> UpdateInArray<ArrDataType>(
            IMongoDbSet<TEntity> entity,
            FilterDefinition<TEntity> filter,
            FieldDefinition<TEntity> dbArrayFieldName,
            ArrDataType oldFieldValue,
            ArrDataType newFieldValue
        ) {
            RemoveFromArray(entity, filter, dbArrayFieldName, new ArrDataType[] { oldFieldValue });
            Task<TEntity> element = AddToArray<ArrDataType>(entity, filter, dbArrayFieldName, new ArrDataType[] { newFieldValue });

            return element;
        }

        public static bool CheckElementExists<FieldDataType>(
            IMongoDbSet<TEntity> entity,
            FieldDefinition<TEntity> dbArrayFieldName,
            FieldDataType element,
            FieldDefinition<TEntity> filterIdFieldName,
            FieldDataType filterIdFieldValue
        ) where FieldDataType : struct {

            FilterDefinition<TEntity> arrayFilter = Builders<TEntity>.Filter.ElemMatch<TEntity>(dbArrayFieldName, Builders<TEntity>.Filter.Eq(dbArrayFieldName.ToString(), element));

            FilterDefinition<TEntity> filter = filterIdFieldName == null
                ? arrayFilter
                : Builders<TEntity>.Filter
                    .Eq(filterIdFieldName.ToString(), filterIdFieldValue)
                    & arrayFilter;
          
            long count = entity.Find(filter)
                .Project(Builders<TEntity>.Projection.Include("_id"))
                .Count();

            return count == 0;
        }

        public static IEnumerable<int> ArrayIntSplice(
            IMongoDbSet<TEntity> entity,
            string dbArrayFieldName,
            FilterDefinition<TEntity> filter,
            int count = 10,
            int skip = 0
        ) {
            IFindFluent<TEntity, TEntity> query = entity.Find(filter);

            ProjectionDefinition<TEntity> projection = Builders<TEntity>.Projection.Slice(dbArrayFieldName, skip, count);
            return query.FirstOrDefault() == null
                   ? null
                   : query
                     .Project(projection)
                     .FirstOrDefault()
                     .GetValue(dbArrayFieldName)
                     .AsBsonArray
                     .Select(e => e.ToInt32());
        }
    }
}
