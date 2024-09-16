using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Api.Data.Entities;

public class OrderEntity
{
    
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    // [ForeignKey(nameof(CustomerId))]
    public CustomerEntity CustomerEntity { get; set; } = null!;
    
}

    // FLUENT APIb 

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.Property(x => x.OrderDate).HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.CustomerEntity).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId);

        builder.Property(nameof(OrderEntity.OrderNumber)).IsRequired();
    }
}
