using Data;

namespace Application.Models;

public class ProductModel
{
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<ProductCharacteristicModel> Characteristics { get; set; } = new List<ProductCharacteristicModel>();
}