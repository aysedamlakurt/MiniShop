namespace MiniShop.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
}

public class CategoryUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
