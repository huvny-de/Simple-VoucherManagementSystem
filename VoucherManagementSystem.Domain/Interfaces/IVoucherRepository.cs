using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Domain.Interfaces;

public interface IVoucherRepository : IRepository<Voucher>
{
    Task<IEnumerable<Voucher>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Voucher>> GetByPromotionIdAsync(string promotionId);
    Task<Voucher?> GetByCodeAsync(string code);
    Task<IEnumerable<Voucher>> GetUsedVouchersAsync();
    Task<IEnumerable<Voucher>> GetUnusedVouchersAsync();
}
