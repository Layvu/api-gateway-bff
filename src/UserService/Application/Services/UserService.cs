using UserService.Application.Interfaces;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repository, ILogger<UserService> logger)
    {
        _userRepository = repository;
        _logger = logger;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user == null)
        {
            _logger.LogWarning("User with id {Id} not found in DB", id);
            return null; 
        }

        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<string> CreateUserAsync(string name, string email)
    {
        var newUser = new User(Guid.NewGuid().ToString(), name, email);
        
        await _userRepository.CreateAsync(newUser);
        
        _logger.LogInformation("User created with ID {Id}", newUser.Id);
        return newUser.Id;
    }
}