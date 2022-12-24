using Application.Models;
using Data;
using MediatR;

namespace Application.Auth
{
    public class LoginCommand : IRequest<(CustomerModel, string)>
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
