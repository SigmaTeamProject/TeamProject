namespace Data;

public class StorageItem : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Amount { get; set; }
}