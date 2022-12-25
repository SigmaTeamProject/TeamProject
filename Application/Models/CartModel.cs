namespace Application.Models;

public class CartModel
{
    public ICollection<BuyProductModel> Products { get; set; }
}