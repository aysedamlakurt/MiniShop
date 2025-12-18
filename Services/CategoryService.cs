using AutoMapper;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Repositories;

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
        var entity = _mapper.Map<Category>(dto);
        await _repo.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}