namespace Application.Models;

public class OrderPreviewModel
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}