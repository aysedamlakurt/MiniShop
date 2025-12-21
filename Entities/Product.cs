namespace MiniShop.Entities;
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public  required string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryID { get; set; }
    public required Category Category { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();


}