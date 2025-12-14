namespace MiniShop.Dtos;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Adet { get; set; }
}

public class CreateOrderRequestDto
{
    public int CustomerId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderResponseDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public List<OrderLineDto> Items { get; set; } = new();
}

public class OrderLineDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Adet { get; set; }
    public decimal Price { get; set; }
}
