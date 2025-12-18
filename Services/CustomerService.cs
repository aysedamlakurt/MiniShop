using AutoMapper;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Repositories;

namespace MiniShop.Services;

public class CustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> RegisterAsync(CustomerRegisterDto dto)
    {
        var existing = await _repository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new Exception("Bu email zaten kayıtlı");

        var customer = _mapper.Map<Customer>(dto);
        // Not: Gerçek senaryoda burada şifre hash'lenmelidir.
        await _repository.AddAsync(customer);

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var customers = await _repository.GetAllAsync();
        return _mapper.Map<List<CustomerDto>>(customers);
    }

    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        return customer == null ? null : _mapper.Map<CustomerDto>(customer);
    }

    public async Task AddAsync(CustomerCreateDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(CustomerUpdateDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}