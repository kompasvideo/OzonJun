using Microsoft.Extensions.DependencyInjection;
namespace PriceCalculator.Bll;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        return services;
    }
}