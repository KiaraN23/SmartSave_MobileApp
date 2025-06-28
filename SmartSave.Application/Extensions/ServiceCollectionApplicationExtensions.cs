using Microsoft.Extensions.DependencyInjection;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Application.Services;

namespace SmartSave.Application.Extensions
{
    public static class ServiceCollectionApplicationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwTokenGeneratorService, JwTokenGeneratorService>();
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
