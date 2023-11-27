using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaxCalculations.Application.Behavious;
using TaxCalculations.Application.Business.TaxCalculator;
using TaxCalculations.Application.Services.TaxCalculator;

namespace TaxCalculations.Application;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient<ITaxCalculatorBusinessAsync, TaxCalculatorBusinessAsync>();
        services.AddTransient<ITaxCalculatorService, TaxCalculatorService>();

    }
}
