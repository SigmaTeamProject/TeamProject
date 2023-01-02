using Application.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Implementation;

public class ModeratorService : IModeratorService
{
    private readonly IConfiguration _configuration;

    public ModeratorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetModeratorLink()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(_configuration["Moderator:Key"]!);
        hash = hash.Replace("/", "");
        return "https://localhost:7297/api/Auth/register?tkn=" + hash;
    }

    public bool Verify(string token)
    {
        if (token != BCrypt.Net.BCrypt.HashPassword(_configuration["Moderator:Key"]!).Replace("/", ""))
        {
            return true;
        }

        return false;
    }
}