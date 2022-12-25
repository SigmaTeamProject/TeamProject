namespace Application.Models;

public class OrderModel
{
    public ICollection<BuyProductModel> ProductModels { get; set; }
    public PaymentConfigModel PaymentConfigModel { get; set; }
}