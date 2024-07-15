using Microsoft.Extensions.DependencyInjection;
using PriceCalculator.Bll.Services;

namespace PriceCalculator.Bll;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddTransient<ICalculationService,CalculationService>();
        
        return services;
    }
}