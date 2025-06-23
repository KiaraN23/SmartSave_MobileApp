namespace SmartSave.Application.Interfaces.Services
{
    public interface IJwTokenGeneratorService
    {
        string GenerateToken(string userId, string firstName, string email);
    }
}
