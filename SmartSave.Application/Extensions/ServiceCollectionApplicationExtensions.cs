using Microsoft.Extensions.DependencyInjection;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Application.Services;

namespace SmartSave.Application.Extensions
{
    public static class ServiceCollectionApplicationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwTokenGeneratorService, JwTokenGeneratorService>();
            services.AddTransient<IGoalService, GoalService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<HttpClient>();
            services.AddTransient<IOpenRouterApiService, OpenRouterApiService>();
            services.AddTransient<IAIServicesService, AIServicesService>();
        }
    }
}
