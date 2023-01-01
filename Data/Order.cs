namespace Data;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<CartItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public Check Check { get; set; }
}