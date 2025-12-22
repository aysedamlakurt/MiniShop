using AutoMapper;
using MiniShop.Dtos;
using MiniShop.Entities;

namespace MiniShop.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // --- CATEGORY ---
        // ReverseMap sayesinde hem Category -> CategoryDto hem tersi çalışır.
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        // --- PRODUCT ---
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ReverseMap();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        // --- CUSTOMER ---
        CreateMap<Customer, CustomerDto>();

        CreateMap<CustomerCreateDto, Customer>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Service'de hashlenecek
    
        CreateMap<CustomerRegisterDto, Customer>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Service'de hashlenecek
    
        CreateMap<CustomerUpdateDto, Customer>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); 
        
        // Kayıt ve Oluşturma işlemlerinde Password -> PasswordHash eşlemesi
        CreateMap<CustomerCreateDto, Customer>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            
        CreateMap<CustomerRegisterDto, Customer>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

        CreateMap<CustomerUpdateDto, Customer>();

        // --- ORDER ---
        // Order -> OrderResponseDto (İlişkili verileri düzleştirme)
        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));

        // OrderItem -> OrderLineDto
        CreateMap<OrderItem, OrderLineDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
    }
}