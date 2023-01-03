namespace Data;

public class Customer : BaseEntity
{
    public PaymentConfig? PaymentConfig {get; set;}
    public string? Role { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Order>? Orders { get; set; } = new List<Order>();
    public DateOnly BirthDay { get; set; }
    public Cart Cart { get; set; }
}