using MongoDB.Driver;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Infrastructure.Repositories;

public class MongoRepository<T> : BaseRepository<T> where T : class
{
    public MongoRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
}