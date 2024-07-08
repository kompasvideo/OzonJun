using System.Dynamic;
using TestingInfrastructure.Creators;
using Xunit;

namespace PriceCalculator.UnitTests.ServicesTests;

public class CalculationServiceTests
{
    [Fact]
    public async Task SaveCalculation_Success()
    {
        // Arrange
        const int goodsCount = 5;

        var userId = Create.RandomId();
        var calculationId = Create.RandomId();

        var goodModels = GoodModelFaker.Generate(goodsCount)
            .TaArray();

        var goods = goodModels
            .Select(x => new GoodEntityFaker.Generate().Single()
                .WithUserId(userId)
                .WithHeight(x.Height)
                .WithWidth(x.Width)
                .WithLength(x.Length)
                .WithWeight(x.Weight))
            .ToArray();
        var goodIds = goods.Select(x => x.Id)
            .ToArray();

        var calculationModel = CalculationModelFaker.Generate()
            .Single()
            .WithUserId(userId)
            .WithGoods(goodModels);
        
    }
}