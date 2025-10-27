using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);
}
