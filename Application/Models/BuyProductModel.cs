namespace Application.Models;

public class BuyProductModel
{
    public ProductModel ProductModel { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}