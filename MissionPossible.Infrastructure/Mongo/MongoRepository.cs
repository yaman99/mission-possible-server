using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MissionPossible.Domain.Common;
using MissionPossible.Shared.Types;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MissionPossible.Infrastructure.Mongo
{
    public class MongoRepository<TEntity, T> : IMongoRepository<TEntity, T> where TEntity : IIdentifiable<T>
    {
        public IMongoCollection<TEntity> Collection { get; }

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<TEntity>(collectionName);

        }

        public async Task<TEntity> GetAsync(T id)
        {
            return await GetAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await Collection.Find(predicate).SingleOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await Collection.Find(predicate).ToListAsync();

        public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
                TQuery query) where TQuery : PagedQueryBase
            => await Collection.AsQueryable().Where(predicate).PaginateAsync(query);

        public async Task AddAsync(TEntity entity)
            => await Collection.InsertOneAsync(entity);

        public async Task UpdateAsync(TEntity entity)
            => await Collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);

        public async Task DeleteAsync(T id)
            => await Collection.DeleteOneAsync(e => e.Id.Equals(id));

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await Collection.Find(predicate).AnyAsync();

        public async Task<IList<TEntity>> GetAll()
        => await Collection.AsQueryable().ToListAsync();

        public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, bool isDescendingOrder, TQuery query) where TQuery : PagedQueryBase
        {
            if (isDescendingOrder)
            {
                return await Collection.AsQueryable().Where(predicate).OrderByDescending(orderBy).PaginateAsync(query);
            }
            return await Collection.AsQueryable().Where(predicate).OrderBy(orderBy).PaginateAsync(query);
        }
    }
}