using AutoBogus;
using Bogus;

namespace PriceCalculator.UnitTests.Fakers;

public class CalculationDeliveryPriceCommandFaker
{
    private static readonly object Lock = new();
    
    private static readonly Faker<CalculationDeliveryPriceCommand> Faker 
        = new AutoFaker<CalculationDeliveryPriceCommand>()
            .RuleFor(x => x.UserId, f => f.Random.Long(0L))
            .RuleFor(x => x.Goods, f => f.GoodModelFaker.Generate(f.Random.Int(1, 4)).ToArray());

    public static CalculationDeliveryPriceCommand Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }
    
    
}