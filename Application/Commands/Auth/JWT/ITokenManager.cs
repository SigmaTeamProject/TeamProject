using Data;

namespace Application.Commands.Auth.JWT
{
    public interface ITokenManager
    {
        string GenerateToken(Customer customer);
    }
}
