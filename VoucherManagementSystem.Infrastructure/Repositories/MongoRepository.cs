using System.Linq.Expressions;
using MongoDB.Driver;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Infrastructure.Repositories;

public class MongoRepository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entity));
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public virtual async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public virtual async Task<bool> ExistsAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var count = await _collection.CountDocumentsAsync(filter);
        return count > 0;
    }

    public virtual async Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _collection.CountDocumentsAsync(_ => true);
        
        return await _collection.CountDocumentsAsync(predicate);
    }

    private static string GetIdValue(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        return idProperty?.GetValue(entity)?.ToString() ?? string.Empty;
    }
}
