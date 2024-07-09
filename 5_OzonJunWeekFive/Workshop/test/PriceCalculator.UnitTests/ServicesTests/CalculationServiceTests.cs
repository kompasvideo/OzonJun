using System.Data;
using PriceCalculator.Bll.Services;
using PriceCalculator.UnitTests.Builders;
using PriceCalculator.UnitTests.Fakers;
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
        // Arrange
        var goodModels = GoodModelFaker.Generate(5)
            .TaArray();
        
        var builder = new CalculationServiceBuilder();
        var service = builder.Build();
        
        // Act
        var price = service.CalculationPriceByVolume(goodModels, out var volume);
        
        // Asserts
        volume.Should().BeApproximately(goodModels.Sum(x => x.Height * x.Width * x.Length), price);
        price.Should().Be((decimal)volume * CalculationService.VolumeToPriceRatio);
    }
    
    [Fact]
    public void CalculationPriceByWeight_Success()
    {
        // Arrange
        var goodModels = GoodModelFaker.Generate(5)
            .TaArray();
        
        var builder = new CalculationServiceBuilder();
        var service = builder.Build();
        
        // Act
        var price = service.CalculationPriceByWeight(goodModels, out var weight);
        
        // Asserts
        weight.Should().Be(goodModels.Sum(x => x.Weight));
        price.Should().Be((decimal)weight * CalculationService.WeightToPriceRatio);
    }
    
    [Fact]
    public async Task QueryCalculations_Success()
    {
        
    }
}