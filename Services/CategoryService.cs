using MiniShop.Entities;
using MiniShop.Repositories;

namespace MiniShop.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Category>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Category?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task AddAsync(Category c) => _repo.AddAsync(c);

    public Task UpdateAsync(Category c) => _repo.UpdateAsync(c);

    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}
