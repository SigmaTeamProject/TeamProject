namespace Application.Models;

public class OrderPreviewModel
{
    public IEnumerable<BuyProductModel> ProductPreviewModels { get; set; } = new List<BuyProductModel>();
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}