using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _service;
    private readonly IMapper _mapper;

    public CustomersController(CustomerService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CustomerRegisterDto dto)
    {
        var result = await _service.RegisterAsync(dto);
        return Ok(result);
    }

    // GET: api/Customers
    [HttpGet]
    public async Task<ActionResult<List<CustomerDto>>> GetAll()
    {
        // Map işlemi artık servisin içinde yapılıyor
        var dtoList = await _service.GetAllAsync(); 
        return Ok(dtoList);
    }
    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);
        if (customer == null)
            return NotFound();

        var dto = _mapper.Map<CustomerDto>(customer);
        return Ok(dto);
    }

    // POST: api/Customers
    [HttpPost]
    public async Task<ActionResult> Create(CustomerCreateDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var entity = _mapper.Map<Customer>(dto); // Password -> PasswordHash map’i MappingProfile’da
        await _service.AddAsync(entity);
        return Ok("Customer eklendi");
    }

    // PUT: api/Customers/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CustomerUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Id uyumsuz");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        var entity = _mapper.Map<Customer>(dto);
        await _service.UpdateAsync(entity);

        return Ok("Customer güncellendi");
    }

    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _service.DeleteAsync(id);
        return Ok("Customer silindi");
    }
}
