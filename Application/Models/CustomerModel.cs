namespace Application.Models;

public class CustomerModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public CheckoutModel Checkout { get; set; }
}