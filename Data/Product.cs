namespace Data;

public class Product : BaseEntity
{
    public StorageItem? StorageItem { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = String.Empty;
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<ProductCharacteristic> Characteristics { get; set; }
}