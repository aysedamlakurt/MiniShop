using MiniShop.Dtos;
using MiniShop.Repositories;
using AutoMapper;
using MiniShop.Entities;

namespace MiniShop.Services;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepo, ICustomerRepository customerRepo, IProductRepository productRepo, IMapper mapper)
    {
        _orderRepository = orderRepo;
        _customerRepository = customerRepo;
        _productRepository = productRepo;
        _mapper = mapper;
    }

    public async Task<string> CreateOrderAsync(CreateOrderRequestDto request)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null) return "Müşteri bulunamadı.";

        var order = new Order { CustomerId = request.CustomerId, Items = new List<OrderItem>() };

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null || product.Stock < item.Adet) return $"Stok yetersiz veya ürün yok: {item.ProductId}";

            product.Stock -= item.Adet;
            order.Items.Add(new OrderItem { ProductId = product.Id, Adet = item.Adet, Price = product.Price });
            order.TotalAmount += product.Price * item.Adet;
        }

        await _orderRepository.AddAsync(order);
        return $"Sipariş No: {order.Id} başarıyla oluşturuldu.";
    }

    public async Task<List<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<List<OrderResponseDto>>(orders);
    }
}