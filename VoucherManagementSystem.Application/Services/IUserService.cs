using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Application.Services;

public interface IUserService
{
    User Create(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth, string address);
    string GetFullName(User user);
    void UpdateProfile(User user, string firstName, string lastName, string phoneNumber, string address);
    void Deactivate(User user);
    void RecordLogin(User user);
}
