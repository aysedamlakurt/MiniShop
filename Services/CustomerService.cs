using MiniShop.Dtos;
using MiniShop.Repositories;
using AutoMapper;
using MiniShop.Entities;
using BC = BCrypt.Net.BCrypt;  // ← Alias kullan

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
        if (existing != null) throw new Exception("Bu email zaten kayıtlı.");
        
        var customer = _mapper.Map<Customer>(dto);
        
        // ✅ ŞİFREYİ HASHLE!
        customer.PasswordHash = BC.HashPassword(dto.Password);
        
        await _repository.AddAsync(customer);
        return _mapper.Map<CustomerDto>(customer);
    }
    
    // ✅ YENİ - LOGIN METODU
    public async Task<CustomerDto> LoginAsync(string email, string password)
    {
        var customer = await _repository.GetByEmailAsync(email);
        if (customer == null)
            throw new UnauthorizedAccessException("Email veya şifre hatalı.");
        
        // ✅ ŞİFREYİ DOĞRULA!
        bool isPasswordValid = BC.Verify(password, customer.PasswordHash);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Email veya şifre hatalı.");
        
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
    
    public async Task UpdateAsync(CustomerUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null) throw new KeyNotFoundException("Müşteri bulunamadı.");
        
        _mapper.Map(dto, existing);
        await _repository.UpdateAsync(existing);
    }
    
    // ✅ YENİ - ŞİFRE DEĞİŞTİRME
    public async Task ChangePasswordAsync(int customerId, string oldPassword, string newPassword)
    {
        var customer = await _repository.GetByIdAsync(customerId);
        if (customer == null) throw new KeyNotFoundException("Müşteri bulunamadı.");
        
        if (!BC.Verify(oldPassword, customer.PasswordHash))
            throw new UnauthorizedAccessException("Mevcut şifre hatalı.");
        
        customer.PasswordHash = BC.HashPassword(newPassword);
        await _repository.UpdateAsync(customer);
    }
    
    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}