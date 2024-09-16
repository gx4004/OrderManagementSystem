using App.Api.Data;
using App.Api.Data.Entities;
using App.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public CustomerController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]

    public async Task<IActionResult> Get()
    {
        var customers = await _dbContext.CustomerEntities.Include(a => a.Orders).ToListAsync();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var customer = await _dbContext.CustomerEntities.Include(a => a.Orders).FirstOrDefaultAsync(a => a.Id == id);

        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CustomerEntity customerEntity)
    {
        _dbContext.CustomerEntities.Add(customerEntity);

        await _dbContext.SaveChangesAsync();

        return Ok(customerEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CustomerEntity customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(customer).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var customer = await _dbContext.CustomerEntities.FindAsync(id);
        if (id != customer.Id)
        {
            return BadRequest();
        }

        _dbContext.CustomerEntities.Remove(customer);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{id}/orders")]

    public async Task<IActionResult> GetOrder([FromRoute] int id)
    {
        var customerOrders = _dbContext.OrderEntities.Where(a => a.CustomerId == id).Select(a => new CustomerOrderModel
        {
            CustomerId = a.CustomerId,
            CustomerName = a.CustomerEntity.Name,
            OrderNumber = a.OrderNumber,
            OrderDate = a.OrderDate
        }).ToListAsync();

        return Ok(customerOrders);

    }
}