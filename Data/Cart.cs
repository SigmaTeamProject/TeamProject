namespace Data;

public class Cart
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Order? Order { get; set; }
    public ICollection<StorageItem>? Items { get; set; }
}