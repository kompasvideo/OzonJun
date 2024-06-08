using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PriceCalculator.Dal;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        return services;
    }
    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services,
        IConfigurationRoot config)
    {
        return services;
    }
}