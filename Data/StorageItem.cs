namespace Data;

public class StorageItem : BaseEntity
{
    public Product Product { get; set; }
    public int Amount { get; set; }
}