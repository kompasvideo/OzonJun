namespace PriceCalculator.Dal.Settings;

public record DalOptions
{
    public string ConnectionString { get; init; } = string.Empty;
}