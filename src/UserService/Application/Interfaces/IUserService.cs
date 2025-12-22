using UserService.Domain.Models;

namespace UserService.Application.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string id);
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<string> CreateUserAsync(string name, string email);
}