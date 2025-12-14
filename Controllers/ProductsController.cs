using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;
    private readonly IMapper _mapper;

    public ProductsController(ProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var products = await _service.GetAllAsync();              // Entity listesi
        var dtoList = _mapper.Map<List<ProductDto>>(products);    // Entity -> DTO
        return Ok(dtoList);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        var dto = _mapper.Map<ProductDto>(product);
        return Ok(dto);
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult> Create(ProductCreateDto dto)
    {
        // FluentValidation burada devreye giriyor (ProductCreateDtoValidator)
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var entity = _mapper.Map<Product>(dto);   // DTO -> Entity
        await _service.AddAsync(entity);

        // İstersen CreatedAtAction da dönebilirsin, şimdilik basit:
        return Ok("Product eklendi");
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ProductUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Id uyumsuz");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        // DTO -> Entity (tüm alanları güncelleyerek)
        var entity = _mapper.Map<Product>(dto);
        await _service.UpdateAsync(entity);

        return Ok("Product güncellendi");
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _service.DeleteAsync(id);
        return Ok("Product silindi");
    }
}
