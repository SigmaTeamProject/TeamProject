namespace Data;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public decimal TotalPrice { get; set; }
}