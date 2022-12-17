using Microsoft.AspNet.Identity.EntityFramework;

namespace Data;

public class Customer : IdentityUser
{
    public PaymentConfig PaymentConfig {get; set;}
}