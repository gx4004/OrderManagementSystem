namespace App.Api.Models;

public class CustomerOrderModel
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public DateTime OrderDate { get; set; }
}