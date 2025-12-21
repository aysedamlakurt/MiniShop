using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Entities;

namespace MiniShop.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly MiniShopDbContext _context;

    public ProductRepository(MiniShopDbContext context)
    {
        _context = context;
    }

   public async Task<List<Product>> GetAllAsync() =>
    await _context.Products
        .Include(p => p.Category) // Kategori nesnesini yükle
        .ToListAsync();

public async Task<Product?> GetByIdAsync(int id) =>
    await _context.Products
        .Include(p => p.Category) // Kategori nesnesini yükle
        .FirstOrDefaultAsync(p => p.Id == id);
    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var p = await _context.Products.FindAsync(id);
        if (p != null)
        {
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}
