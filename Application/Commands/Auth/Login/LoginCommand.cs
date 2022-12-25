using Application.Models;
using MediatR;

namespace Application.Commands.Auth.Login
{
    public class LoginCommand : IRequest<(CustomerModel, string)>
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}