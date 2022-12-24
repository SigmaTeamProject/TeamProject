using MediatR;

namespace Application.Commands.Auth
{
    public class RegisterUserCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
