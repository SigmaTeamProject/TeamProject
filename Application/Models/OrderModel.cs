namespace Application.Models;

public class OrderModel
{
    public decimal TotalAmount { get; set; }
    public IEnumerable<BuyProductModel> Products { get; set; }
    public IEnumerable<PaymentConfigModel> PossiblePaymentMethods { get; set; }
}