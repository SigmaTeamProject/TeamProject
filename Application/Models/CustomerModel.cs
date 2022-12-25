namespace Application.Models;

public class CustomerModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public OrderModel Order { get; set; }
}