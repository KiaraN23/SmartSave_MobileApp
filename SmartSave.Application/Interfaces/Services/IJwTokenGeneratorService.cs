namespace SmartSave.Application.Interfaces.Services
{
    public interface IJwTokenGeneratorService
    {
        string GenerateToken(string firstName, string email);
    }
}
