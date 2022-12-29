namespace Data;

public class PaymentConfig : BaseEntity
{
    public Customer? Customer { get; set; }
    public int CustomerId { get; set; }
    public string Type { get; set; }
    public Customer Customer { get; set; }
}