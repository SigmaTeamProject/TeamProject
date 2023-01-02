namespace Application.Models;

public class CheckoutModel
{
    public decimal TotalPrice { get; set; }
    public IEnumerable<BuyProductModel> Products { get; set; } = new List<BuyProductModel>();
    public IEnumerable<PaymentConfigModel> PossiblePaymentMethods { get; set; }
}