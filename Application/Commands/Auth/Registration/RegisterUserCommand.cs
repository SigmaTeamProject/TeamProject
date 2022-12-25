using Application.Models;
using MediatR;

namespace Application.Commands.Auth.Registration
{
    public class RegisterUserCommand : IRequest<(CustomerModel, string)>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
