namespace Data;

public class StorageItem
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Amount { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Cart>? Carts { get; set; }
}