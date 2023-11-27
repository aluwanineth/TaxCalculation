using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculations.Application.Contracts.Repositories;
using TaxCalculations.Application.Contracts.Services;
using TaxCalculations.Persistence.Contexts;
using TaxCalculations.Persistence.Repositories;
using TaxCalculations.Persistence.Services;

namespace TaxCalculations.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
        services.AddDbContext<TaxCalculationsDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection").Replace("[DataDirectory]", path),
            b => b.MigrationsAssembly(typeof(TaxCalculationsDbContext).Assembly.FullName)));

        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
        services.AddTransient<ITaxCalculatorTypeRepositoryAsync, TaxCalculatorTypeRepositoryAsync>();
        services.AddTransient<ITaxCalculationRepositoryAsync, TaxCalculationRepositoryAsync>();
        services.AddTransient<IDateTimeService, DateTimeService>();

    }
}
