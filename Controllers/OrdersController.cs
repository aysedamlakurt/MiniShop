using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Services;

namespace MiniShop.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;
    public OrdersController(OrderService service) => _service = service;

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequestDto request)
    {
        var result = await _service.CreateOrderAsync(request);
        return result.Contains("başarıyla") ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
}