namespace Data;

public class Order : BaseEntity
{
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<StorageItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
}