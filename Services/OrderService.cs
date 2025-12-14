using MiniShop.Entities;
using MiniShop.Repositories;

namespace MiniShop.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Sipariş oluşturur:
    /// - Müşteri var mı kontrol eder
    /// - Her ürün için stok ve varlık kontrolü yapar
    /// - Stok düşer
    /// - Order ve OrderItem listesi oluşturur
    /// - Tek seferde veritabanına kaydeder
    /// </summary>
    public async Task<string> CreateOrderAsync(int customerId, List<(int ProductId, int Adet)> items)
    {
        // 1) Müşteri kontrolü
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null)
            return $"Müşteri bulunamadı. Id = {customerId}";

        // 2) Ürün listesi boş mu?
        if (items == null || items.Count == 0)
            return "Sipariş için en az bir ürün seçmelisiniz.";

        // 3) Sipariş nesnesi oluştur
        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,   // PostgreSQL timestamp with time zone için UTC
            TotalAmount = 0,
            Items = new List<OrderItem>()
        };

        // 4) Her ürün için kontrol ve OrderItem oluşturma
        foreach (var (productId, adet) in items)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return $"Ürün bulunamadı. ProductId = {productId}";

            if (adet <= 0)
                return $"Geçersiz adet: {adet} (ProductId = {productId})";

            if (product.Stock < adet)
                return $"Yetersiz stok: {product.Name} (Stok: {product.Stock}, İstenen: {adet})";

            // Stok düş
            product.Stock -= adet;
            await _productRepository.UpdateAsync(product);

            // OrderItem sadece order.Items içine ekleniyor
            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Adet = adet,
                Price = product.Price
            };

            // Toplam tutara ekle
            order.TotalAmount += product.Price * adet;

            // Order'ın item listesine ekle
            order.Items.Add(orderItem);
        }

        // 5) Tek seferde Order kaydet -> EF OrderItems'ı da insert eder
        await _orderRepository.AddAsync(order);

        return $"Sipariş oluşturuldu. OrderId = {order.Id}, Toplam Tutar = {order.TotalAmount}₺";
    }

    /// <summary>
    /// Tüm siparişleri döner.
    /// OrdersController GET /api/Orders bunu kullanıyor.
    /// </summary>
    public Task<List<Order>> GetAllAsync()
        => _orderRepository.GetAllAsync();

    /// <summary>
    /// Id'ye göre sipariş döner.
    /// OrdersController GET /api/Orders/{id} bunu kullanıyor.
    /// </summary>
    public Task<Order?> GetByIdAsync(int id)
        => _orderRepository.GetByIdAsync(id);
}
