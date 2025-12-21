using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Services;

namespace MiniShop.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;
    public ProductsController(ProductService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        await _service.AddAsync(dto);
        return Ok("Ürün eklendi.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("ID uyuşmazlığı.");
        await _service.UpdateAsync(dto);
        return Ok("Ürün güncellendi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok("Ürün silindi.");
    }
}