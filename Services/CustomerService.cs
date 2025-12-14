using AutoMapper;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Repositories;

namespace MiniShop.Services;

public class CustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

      private readonly ICustomerRepository _repo;

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

        await _repository.AddAsync(customer);

        return _mapper.Map<CustomerDto>(customer);
    }
    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Customer>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Customer?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task AddAsync(Customer c) => _repo.AddAsync(c);

    public Task UpdateAsync(Customer c) => _repo.UpdateAsync(c);

    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}
