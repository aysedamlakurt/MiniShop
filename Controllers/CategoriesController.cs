using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _service;
    private readonly IMapper _mapper;

    public CategoriesController(CategoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var categories = await _service.GetAllAsync();
        var dtoList = _mapper.Map<List<CategoryDto>>(categories);
        return Ok(dtoList);
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category == null)
            return NotFound();

        var dto = _mapper.Map<CategoryDto>(category);
        return Ok(dto);
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<ActionResult> Create(CategoryCreateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var entity = _mapper.Map<Category>(dto);
        await _service.AddAsync(entity);
        return Ok("Category eklendi");
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CategoryUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Id uyumsuz");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        var entity = _mapper.Map<Category>(dto);
        await _service.UpdateAsync(entity);

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
