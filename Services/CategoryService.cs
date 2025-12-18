using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _service;

    // IMapper bağımlılığını buradan kaldırdık çünkü servis bu işi üstleniyor
    public CategoriesController(CategoryService service)
    {
        _service = service;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        // Servis doğrudan DTO listesi dönüyor
        var dtoList = await _service.GetAllAsync();
        return Ok(dtoList);
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null)
            return NotFound();

        return Ok(dto);
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<ActionResult> Create(CategoryCreateDto dto)
    {
        // FluentValidation Program.cs'de kayıtlı olduğu için otomatik çalışır
        await _service.AddAsync(dto);
        return Ok("Category eklendi");
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CategoryUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Id uyumsuz");

        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _service.UpdateAsync(dto);
        return Ok("Category güncellendi");
    }

    // DELETE: api/Categories/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _service.DeleteAsync(id);
        return Ok("Category silindi");
    }
}