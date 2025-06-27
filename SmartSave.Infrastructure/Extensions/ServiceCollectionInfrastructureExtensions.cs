using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Infrastructure.Data.Contexts;
using SmartSave.Infrastructure.Data.Repositories;

namespace SmartSave.Infrastructure.Extensions
{
    public static class ServiceCollectionInfrastructureExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbConfiguration(services, configuration);
            AddRepositories(services);
        }

        #region "Private methods"
        private static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemory"))
            {
                services.AddDbContext<SmartSaveDbContext>(options => options.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                services.AddDbContext<SmartSaveDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("WilmeConnection"),
                        m => m.MigrationsAssembly(typeof(SmartSaveDbContext).Assembly.FullName));
                });
            }
            
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
        #endregion
    }
}
