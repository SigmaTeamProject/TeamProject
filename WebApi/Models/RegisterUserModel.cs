namespace WebApi.Models
{
    public class RegisterUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
