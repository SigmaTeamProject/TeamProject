namespace Data;

public class ProductCharacteristic : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Value { get; set; } = String.Empty;
}