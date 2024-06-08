using FluentMigrator.Runner.Generators.Postgres;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PriceCalculator.Dal.Infrastructure;
using PriceCalculator.Dal.Settings;

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
        // read config
        services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));
        
        // configure postgres types
        Postgres.MapCompositeTypes();
        
        //add migrations
        Postgres.AddMigrations(services);
        
        return services;
    }
}