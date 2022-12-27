namespace Data;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public Cart CartId { get; set; }
    public ICollection<StorageItem> OrderItems { get; set; }
    public decimal OrderPrice { get; set; }
}