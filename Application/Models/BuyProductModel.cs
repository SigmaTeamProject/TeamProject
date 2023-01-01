namespace Application.Models;

public class BuyProductModel
{
    public ProductModel ProductModel { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}