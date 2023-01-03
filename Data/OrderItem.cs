namespace Data;

public class OrderItem
{
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; } 
}