namespace SmartSave.Application.Interfaces.Services
{
    public interface IOpenRouterApiService
    {
        public Task<string> SendMessageAsync(string prompt);
    }
}
