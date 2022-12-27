namespace Data;

public class Product : BaseEntity
{
    public decimal Price { get; set; }
    public string? Name { get; set; }
    public ICollection<ProductCharacteristic> Characteristics { get; set; }
}