using Data;

public class CartItem : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Amount { get; set; }
    public int CartId { get; set; }
    public Cart? Cart { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
}