using System.Dynamic;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using MiniShop.Data;
using MiniShop.Entities;

namespace MiniShop.Data;

public class MiniShopDbContext : DbContext
{
    public MiniShopDbContext(DbContextOptions<MiniShopDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

}
// EF Core tarafından kullanılan veritabanı bağlamı.
// tabloları tanımlar, sorguları çalıştırır, değişiklikleri izler ve veritabanına yazar.