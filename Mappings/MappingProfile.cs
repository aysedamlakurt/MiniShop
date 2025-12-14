using AutoMapper;
using MiniShop.Dtos;
using MiniShop.Entities;

namespace MiniShop.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CATEGORY
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        // PRODUCT
        CreateMap<Product, ProductDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        // CUSTOMER
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerCreateDto, Customer>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Dilersen
        CreateMap<CustomerUpdateDto, Customer>();

        // ORDER → ORDERRESPONSEDTO
        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.Name));

        // ORDERITEM → ORDERLINEDTO
        CreateMap<OrderItem, OrderLineDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<CustomerRegisterDto, Customer>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password)); // Şimdilik hash yok
        CreateMap<Customer, CustomerDto>();
    }
}
