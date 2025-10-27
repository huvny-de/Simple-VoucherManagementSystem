using MongoDB.Driver;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Infrastructure.Repositories;

public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
{
    public VoucherRepository(IMongoDatabase database) : base(database, "vouchers")
    {
    }

    public async Task<IEnumerable<Voucher>> GetByUserIdAsync(string userId)
    {
        var filter = CombineWithSoftDelete(Builders<Voucher>.Filter.Eq("UserId", userId));
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Voucher>> GetByPromotionIdAsync(string promotionId)
    {
        var filter = CombineWithSoftDelete(Builders<Voucher>.Filter.Eq("PromotionId", promotionId));
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<Voucher?> GetByCodeAsync(string code)
    {
        var filter = CombineWithSoftDelete(Builders<Voucher>.Filter.Eq("Code", code));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Voucher>> GetUsedVouchersAsync()
    {
        var filter = CombineWithSoftDelete(Builders<Voucher>.Filter.Eq("IsUsed", true));
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Voucher>> GetUnusedVouchersAsync()
    {
        var filter = CombineWithSoftDelete(Builders<Voucher>.Filter.Eq("IsUsed", false));
        return await _collection.Find(filter).ToListAsync();
    }
}
