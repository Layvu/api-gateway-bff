using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Domain.Models;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(string name, string email)
    {
        var id = await _userService.CreateUserAsync(name, email);
        
        return CreatedAtAction(nameof(GetUser), new { id }, new { id, name, email });
    }
}