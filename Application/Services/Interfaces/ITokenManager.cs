using Data;

namespace Application.Services.Interfaces
{
    public interface ITokenManager
    {
        string GenerateToken(Customer customer);
    }
}
