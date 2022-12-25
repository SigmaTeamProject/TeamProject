namespace Data;

public class Check : BaseEntity
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }
}