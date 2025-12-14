using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Entities;

namespace MiniShop.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly MiniShopDbContext _context;

    public CategoryRepository(MiniShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync() =>
        await _context.Categories.ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) =>
        await _context.Categories.FindAsync(id);

    public async Task AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var c = await _context.Categories.FindAsync(id);
        if (c != null)
        {
            _context.Categories.Remove(c);
            await _context.SaveChangesAsync();
        }
    }
}
