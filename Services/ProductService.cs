public class ProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task AddAsync(ProductCreateDto dto)
    {
        var entity = _mapper.Map<Product>(dto);
        await _repo.AddAsync(entity);
    }

    public async Task UpdateAsync(ProductUpdateDto dto)
    {
        var existing = await _repo.GetByIdAsync(dto.Id);
        if (existing == null) throw new KeyNotFoundException("Ürün bulunamadı.");

        _mapper.Map(dto, existing);
        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}