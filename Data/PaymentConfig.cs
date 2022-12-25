namespace Data;

public class PaymentConfig
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string Type { get; set; }
}