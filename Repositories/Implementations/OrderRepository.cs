using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Entities;

namespace MiniShop.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly MiniShopDbContext _context;

    public OrderRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync() =>
        await _context.Orders
          .Include(o => o.Customer)    
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync();

    public async Task<Order?> GetByIdAsync(int id) =>
        await _context.Orders
          .Include(o => o.Customer) 
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}
