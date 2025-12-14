using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Entities;

namespace MiniShop.Repositories.Implementations;

public class CustomerRepository : ICustomerRepository
{
    private readonly MiniShopDbContext _context;

    public CustomerRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllAsync() =>
        await _context.Customers.ToListAsync();

    public async Task<Customer?> GetByIdAsync(int id) =>
        await _context.Customers.FindAsync(id);

    public async Task AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var c = await _context.Customers.FindAsync(id);
        if (c != null)
        {
            _context.Customers.Remove(c);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(x => x.Email == email);
    }


}
