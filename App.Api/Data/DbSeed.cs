using App.Api.Data.Entities;

namespace App.Api.Data;

public static class DbSeed
{
    public static async Task SeedAsync(AppDbContext dbContext)
    {
        var customer = new CustomerEntity
        {
            Name = "Ali Yilmaz",
            Email = "ali@gmail.com",
            Phone = "123213122"
                
        };

         dbContext.CustomerEntities.Add(customer);
         await dbContext.SaveChangesAsync();

         var order1 = new OrderEntity
         {
             CustomerId = customer.Id,
             OrderNumber = $"ORD-{DateTime.Now:yyyy-MM-dd} - {customer.Id} "
         };
         
         var order2 = new OrderEntity
         {
             CustomerId = customer.Id,
             OrderNumber = $"ORD-{DateTime.Now:yyyy-MM-dd} - {customer.Id} "
         };

         dbContext.OrderEntities.Add(order1);
         dbContext.OrderEntities.Add(order2);
         await dbContext.SaveChangesAsync();

    }
}