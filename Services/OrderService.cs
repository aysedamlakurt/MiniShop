using AutoMapper;
using MiniShop.Dtos;
using MiniShop.Entities;
using MiniShop.Repositories;

namespace MiniShop.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Sipariş oluşturma mantığı:
    /// - Stok kontrolü ve düşümü tek bir transaction içinde yapılır.
    /// - Veri dönüşümleri servis katmanında yönetilir.
    /// </summary>
    public async Task<string> CreateOrderAsync(int customerId, List<(int ProductId, int Adet)> items)
    {
        // 1) Müşteri kontrolü
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null)
            return $"Müşteri bulunamadı. Id = {customerId}";

        if (items == null || items.Count == 0)
            return "Sipariş için en az bir ürün seçmelisiniz.";

        // 2) Sipariş nesnesi oluştur
        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0,
            Items = new List<OrderItem>()
        };

        // 3) Ürün kontrolleri ve Stok Düşümü (Bellek üzerinde)
        foreach (var (productId, adet) in items)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return $"Ürün bulunamadı. ProductId = {productId}";

            if (product.Stock < adet)
                return $"Yetersiz stok: {product.Name} (Mevcut: {product.Stock})";

            // ÖNEMLİ: Stok düşme işlemini döngü içinde veritabanına yansıtmıyoruz.
            // Sadece bellek üzerindeki nesneyi güncelliyoruz.
            product.Stock -= adet;

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Adet = adet,
                Price = product.Price
            };

            order.TotalAmount += product.Price * adet;
            order.Items.Add(orderItem);
        }

        // 4) Atomik Kayıt
        // orderRepository.AddAsync metodu SaveChangesAsync içerdiği için 
        // hem Order/OrderItems eklenir hem de Product stok güncellemeleri tek seferde kaydedilir.
        await _orderRepository.AddAsync(order);

        return $"Sipariş başarıyla oluşturuldu. OrderId = {order.Id}, Toplam Tutar = {order.TotalAmount}₺";
    }

    /// <summary>
    /// Tüm siparişleri DTO olarak döner.
    /// </summary>
    public async Task<List<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<List<OrderResponseDto>>(orders);
    }

    /// <summary>
    /// Spesifik bir siparişi DTO olarak döner.
    /// </summary>
    public async Task<OrderResponseDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order == null ? null : _mapper.Map<OrderResponseDto>(order);
    }
}