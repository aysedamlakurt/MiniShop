using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;
    private readonly IMapper _mapper;

    public OrdersController(OrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // POST: api/Orders/create
    [HttpPost("create")]
    public async Task<ActionResult> CreateOrder(CreateOrderRequestDto request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);   // FluentValidation burada devreye giriyor

        var items = request.Items
            .Select(i => (i.ProductId, i.Adet))
            .ToList();

        var result = await _service.CreateOrderAsync(request.CustomerId, items);

        if (result.StartsWith("Yetersiz") || result.Contains("bulunamadı"))
            return BadRequest(result);

        return Ok(result);
    }

    // GET: api/Orders
    [HttpGet]
    public async Task<ActionResult<List<OrderResponseDto>>> GetAll()
    {
        var orders = await _service.GetAllAsync();                  // List<Order>
        var dtoList = _mapper.Map<List<OrderResponseDto>>(orders);  // List<OrderResponseDto>
        return Ok(dtoList);
    }

    // GET: api/Orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetById(int id)
    {
        var order = await _service.GetByIdAsync(id);
        if (order == null)
            return NotFound();

        var dto = _mapper.Map<OrderResponseDto>(order);
        return Ok(dto);
    }
}
