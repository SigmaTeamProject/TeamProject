namespace Application.Services.Interfaces;

public interface IModeratorService
{
    public string GetModeratorLink();
    public bool Verify(string token);
}