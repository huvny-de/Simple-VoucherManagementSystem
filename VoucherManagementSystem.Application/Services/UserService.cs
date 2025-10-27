using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Application.Services;

public class UserService : IUserService
{
    public User Create(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth, string address)
    {
        return new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            Address = address,
            IsActive = true
        };
    }

    public string GetFullName(User user)
    {
        return $"{user.FirstName} {user.LastName}";
    }

    public void UpdateProfile(User user, string firstName, string lastName, string phoneNumber, string address)
    {
        user.FirstName = firstName;
        user.LastName = lastName;
        user.PhoneNumber = phoneNumber;
        user.Address = address;
        user.UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate(User user)
    {
        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;
    }

    public void RecordLogin(User user)
    {
        user.LastLoginAt = DateTime.UtcNow;
    }
}
