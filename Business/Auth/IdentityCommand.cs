using MediatR;

namespace Business.Auth
{
    public class IdentityCommand : IRequest<string>
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
