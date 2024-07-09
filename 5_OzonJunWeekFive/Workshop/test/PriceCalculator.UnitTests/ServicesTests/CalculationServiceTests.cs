using System.Data;
using TestingInfrastructure.Creators;
using TestingInfrastructure.Fakers;

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

        var calculations = CalculationEntityV1Faker.Generate(1)
            .Select(x => x
                    .WithId(calculationId)
                    .WithUserId(userId)
                    .WithPrice(calculationModel.Price)
                    .WithTotalWeight(calculationModel.TotalWeight)
                    .WithTotalVolume(calculationModel.TotalVolume))
                .ToArray();
        
        var builder = new CalculationServiceBuilder();
        builder.CalculationRepository
            .SetupAddcalculations(new[] { calculationId })
            .SetupCreateTransactionScope();
        builder.GoodsRepository
            .SetupAddGoods(goodIds);
        
        var service = builder.Build();
        
        // Act
        var result = await service.SaveCalculation(calculationModel, default);
        
        // Assert
        result.Should().Be(calculationId);
        service.CalculationRepository
            .VerifyAddWasCalledOnce(calculations)
            .VerifyCreateTransactionScopeWasCalledOnce(IsolationLevel.ReadCommitted);
        service.GoodsRepository
            .VerifyAddWasCalledOnce(goodIds);
        service.VerifyNotOtherCalls();
    }

    [Fact]
    public void CalculationPriceByVolume_Success()
    {
        
    }
}