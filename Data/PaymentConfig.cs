namespace Data;

public class PaymentConfig : BaseEntity
{
    public string Type { get; set; }
    public Customer Customer { get; set; }
}