namespace Data;

public class Customer
{
    public PaymentConfig? PaymentConfig {get; set;}
    public string? Role { get; set; }
    public string Login { get; set; }
    public int Id { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Order> Orders { get; set; }
    public DateOnly BirthDay { get; set; }
    public Cart Cart { get; set; }
    public int CartId { get; set; }
}