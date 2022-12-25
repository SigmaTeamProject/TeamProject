namespace Data;

public class ProductCharacteristic
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}