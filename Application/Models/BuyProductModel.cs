namespace Application.Models;

public class BuyProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}