using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Entities;

namespace MiniShop.Repositories.Implementations;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly MiniShopDbContext _context;

    public OrderItemRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OrderItem item)
    {
        _context.OrderItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderItem>> GetByOrderIdAsync(int orderId) =>
        await _context.OrderItems
            .Where(x => x.OrderId == orderId)
            .Include(x => x.Product)
            .ToListAsync();
}
