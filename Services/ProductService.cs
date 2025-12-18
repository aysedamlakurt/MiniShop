using AutoMapper;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Repositories;

namespace MiniShop.Services;

public class ProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    // Tüm ürünleri ProductDto listesi olarak döner
    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    // Id'ye göre tek bir ürünü ProductDto olarak döner
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    // ProductCreateDto alıp entity'ye dönüştürerek kaydeder
    public async Task AddAsync(ProductCreateDto dto)
    {
        var entity = _mapper.Map<Product>(dto);
        await _repo.AddAsync(entity);
    }

    // ProductUpdateDto alıp entity'ye dönüştürerek günceller
    public async Task UpdateAsync(ProductUpdateDto dto)
    {
        var entity = _mapper.Map<Product>(dto);
        await _repo.UpdateAsync(entity);
    }

    // Ürünü siler
    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
}