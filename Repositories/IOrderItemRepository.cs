using MiniShop.Entities;

namespace MiniShop.Repositories;

public interface IOrderItemRepository
{
    Task AddAsync(OrderItem item);
    Task<List<OrderItem>> GetByOrderIdAsync(int orderId);

   
}