using UserService.Application.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Data;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User("1", "John Doe", "john.doe@example.com"),
        new User("2", "Jane Smith", "jane.smith@test.com"),
        new User("3", "Bob The Builder", "bob@builds.it")
    };

    public Task<User?> GetByIdAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult((IEnumerable<User>)_users);
    }
}