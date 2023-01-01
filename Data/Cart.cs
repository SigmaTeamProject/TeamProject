namespace Data;

public class Cart
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<CartItem>? Items { get; set; }
}