using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(string userId)
    {
        var orders = await _service.GetOrdersByUserIdAsync(userId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string userId, decimal totalAmount)
    {
        var id = await _service.CreateOrderAsync(userId, totalAmount);
        return Ok(new { id, userId, totalAmount });
    }
}