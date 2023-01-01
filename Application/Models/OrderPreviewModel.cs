namespace Application.Models;

public class OrderPreviewModel
{
    public IEnumerable<ProductPreviewModel> ProductPreviewModels { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}