namespace Application.Models;

public class OrderPreviewModel
{
    public int Id { get; set; }
    public IEnumerable<BuyProductModel> ProductPreviewModels { get; set; } = new List<BuyProductModel>();
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}