using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Repositories;
using MiniShop.Repositories.Implementations;
using MiniShop.Services;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MiniShop.Mappings;
using MiniShop.Validators;
using MiniShop.Dtos;


var builder = WebApplication.CreateBuilder(args);

// PostgreSQL DB bağlantısı
builder.Services.AddDbContext<MiniShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository kayıtları
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

// Service kayıtları
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IValidator<CustomerRegisterDto>, CustomerRegisterDtoValidator>();


// Controller ve Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Static files (wwwroot dizinini serve etsin)
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();