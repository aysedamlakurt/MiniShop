using MiniShop.Dtos;
using MiniShop.Repositories;
using AutoMapper;
using MiniShop.Entities;

namespace MiniShop.Services;
public class CategoryService
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        return category == null ? null : _mapper.Map<CategoryDto>(category);
    }

    public async Task AddAsync(CategoryCreateDto dto)
    {
        var entity = _mapper.Map<Category>(dto);
        await _repo.AddAsync(entity);
    }

    public async Task UpdateAsync(CategoryUpdateDto dto)
    {
        var existing = await _repo.GetByIdAsync(dto.Id);
        if (existing == null) throw new KeyNotFoundException("Kategori bulunamadı.");

        _mapper.Map(dto, existing); // Güvenli güncelleme
        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}