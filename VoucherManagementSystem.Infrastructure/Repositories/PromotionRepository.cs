using MongoDB.Driver;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Infrastructure.Repositories;

public class PromotionRepository : BaseRepository<Promotion>, IPromotionRepository
{
    public PromotionRepository(IMongoDatabase database) : base(database, "promotions")
    {
    }

    public async Task<Promotion?> GetByCodeAsync(string code)
    {
        var filter = CombineWithSoftDelete(Builders<Promotion>.Filter.Eq("Code", code));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync()
    {
        var now = DateTime.UtcNow;
        var activeFilter = Builders<Promotion>.Filter.And(
            Builders<Promotion>.Filter.Eq("IsActive", true),
            Builders<Promotion>.Filter.Lte("StartDate", now),
            Builders<Promotion>.Filter.Gte("EndDate", now)
        );
        var filter = CombineWithSoftDelete(activeFilter);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Promotion>> GetPromotionsByUserIdAsync(string userId)
    {
        var userFilter = Builders<Promotion>.Filter.Or(
            Builders<Promotion>.Filter.Size("ApplicableUserIds", 0),
            Builders<Promotion>.Filter.In("ApplicableUserIds", new[] { userId })
        );
        var filter = CombineWithSoftDelete(userFilter);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Promotion>> GetExpiredPromotionsAsync()
    {
        var now = DateTime.UtcNow;
        var expiredFilter = Builders<Promotion>.Filter.Lt("EndDate", now);
        var filter = CombineWithSoftDelete(expiredFilter);
        return await _collection.Find(filter).ToListAsync();
    }
}
