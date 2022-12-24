namespace Application.Dtos
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
