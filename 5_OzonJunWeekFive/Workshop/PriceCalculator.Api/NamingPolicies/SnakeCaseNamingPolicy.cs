using System.Text.Json;
using PriceCalculator.Api.Extensions;

namespace PriceCalculator.Api.NamingPolicies;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) =>
        name.ToSnakeCase();
}