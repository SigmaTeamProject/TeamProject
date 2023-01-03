namespace Application.Models;

public class OrderModel : OrderPreviewModel
{
    public IEnumerable<BuyProductModel> ProductPreviewModels { get; set; } = new List<BuyProductModel>();
}