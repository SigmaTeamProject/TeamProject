using Data;


namespace Application.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductCharacteristic> Characteristics { get; set; }
        public int Id { get; set; }
    }
}
