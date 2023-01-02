namespace Application.Models;

public class CheckModel
{
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = String.Empty;
    public IEnumerable<BuyProductModel> ProductModels { get; set; }
    public bool IsSuccess { get; set; }
}