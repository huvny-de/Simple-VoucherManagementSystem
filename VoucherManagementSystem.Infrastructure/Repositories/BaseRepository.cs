using System.Linq.Expressions;
using MongoDB.Driver;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    protected BaseRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    // Global soft delete filter
    protected virtual FilterDefinition<T> GetSoftDeleteFilter()
    {
        return Builders<T>.Filter.Ne("IsDeleted", true);
    }

    protected virtual FilterDefinition<T> CombineWithSoftDelete(FilterDefinition<T> filter)
    {
        return Builders<T>.Filter.And(filter, GetSoftDeleteFilter());
    }

    protected virtual FilterDefinition<T> CombineWithSoftDelete(Expression<Func<T, bool>> predicate)
    {
        return Builders<T>.Filter.And(
            Builders<T>.Filter.Where(predicate),
            GetSoftDeleteFilter()
        );
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        var filter = CombineWithSoftDelete(Builders<T>.Filter.Eq("Id", id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var filter = GetSoftDeleteFilter();
        return await _collection.Find(filter).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var filter = CombineWithSoftDelete(predicate);
        return await _collection.Find(filter).ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", GetIdValue(entity));
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public virtual async Task DeleteAsync(string id)
    {
        // Soft delete - set IsDeleted = true
        var filter = Builders<T>.Filter.Eq("Id", id);
        var update = Builders<T>.Update.Set("IsDeleted", true).Set("UpdatedAt", DateTime.UtcNow);
        await _collection.UpdateOneAsync(filter, update);
    }

    public virtual async Task<bool> ExistsAsync(string id)
    {
        var filter = CombineWithSoftDelete(Builders<T>.Filter.Eq("Id", id));
        var count = await _collection.CountDocumentsAsync(filter);
        return count > 0;
    }

    public virtual async Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            var filter = GetSoftDeleteFilter();
            return await _collection.CountDocumentsAsync(filter);
        }
        
        var combinedFilter = CombineWithSoftDelete(predicate);
        return await _collection.CountDocumentsAsync(combinedFilter);
    }

    protected static string GetIdValue(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        return idProperty?.GetValue(entity)?.ToString() ?? string.Empty;
    }
}
