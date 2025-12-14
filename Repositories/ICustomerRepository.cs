using MiniShop.Entities;

namespace MiniShop.Repositories;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task AddAsync(Customer customer);
     Task<Customer?> GetByEmailAsync(string email);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int id);

}