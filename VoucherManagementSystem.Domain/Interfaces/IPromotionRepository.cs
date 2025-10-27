using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Domain.Interfaces;

public interface IPromotionRepository : IRepository<Promotion>
{
    Task<Promotion?> GetByCodeAsync(string code);
    Task<IEnumerable<Promotion>> GetActivePromotionsAsync();
    Task<IEnumerable<Promotion>> GetPromotionsByUserIdAsync(string userId);
    Task<IEnumerable<Promotion>> GetExpiredPromotionsAsync();
}
