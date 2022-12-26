using Microsoft.AspNet.Identity.EntityFramework;

namespace Data;

public class Customer : IdentityUser
{
    public int PaymentConfigId { get; set; }
    public PaymentConfig? PaymentConfig {get; set;}
    public string Login { get; set; }
    public int Id { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Order> Orders { get; set; }
    public DateOnly BirthDay { get; set; }
}