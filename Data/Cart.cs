namespace Data;

public class Cart
{
    public Customer Customer { get; set; }
    public ICollection<StorageItem> Items { get; set; }
}