using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interfaces;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, decimal price)
    {
        var id = await _service.CreateProductAsync(name, price);
        return CreatedAtAction(nameof(Get), new { id }, new { id, name, price });
    }
}