using MiniShop.Entities;
using MiniShop.Repositories;

namespace MiniShop.Services;

public class ProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Product>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task AddAsync(Product p) => _repo.AddAsync(p);
    public Task UpdateAsync(Product p) => _repo.UpdateAsync(p);
    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}
