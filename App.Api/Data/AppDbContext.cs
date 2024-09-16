using App.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<CustomerEntity> CustomerEntities { get; set; }
    public DbSet<OrderEntity> OrderEntities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}