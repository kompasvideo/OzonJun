using PriceCalculator.Bll.Commands;
using PriceCalculator.UnitTests.Fakers;
using TestingInfrastructure.Creators;

namespace PriceCalculator.UnitTests.HandlersTests;

public class CalculateDeliveryPriceCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCalls()
    {
        // Arrange
        var userId = Create.RandomId();
        var calculationId = Create.RandomId();
        
        var command = new CalculateDeliveryPriceCommandFaker.Generate()
            .WithUserId(userId);

        var calculationModel = CalculationModelFaker.Generate()
            .Single()
            .WithUserId(userId)
            .WithGoods(command.Goods);

        var builder = new CalculateDeliveryPriceHandlerBuilder();
        builder.CalculationService
            .SetupCalculationPriceByWeight(calculationModel.TotalWeight, calculationModel.Price)
            .SetupCalculationPriceByVolume(calculationModel.TotalVolume, calculationModel.Price)
            .SetupSaveCalculation(calculationId);
        
        var handler = builder.Build();
        
        // Act
        var result = await handler.Handle(command, default);
        
        // Assert
        handler.CalculationService
            .VerifySaveCalculationWasCalledOnce(calculationModel)
            .VerifyCalculatePriceByVolumeWasCalledOnce(calculationModel.Goods)
            .VerifyCalculatePriceByWeightWasCalledOnce(calculationModel.Goods);
        
        handler.VerifyNotOtherCalls();
        
        result.CalculationId.Should().Be(calculationId);
        result.Price.Should().Be(calculationModel.Price);
    }

    [Fact]
    public async Task Handle_ResultPriceIsMaxOfTwo()
    {
        // Arrange
        var userId = Create.RandomId();
        var volume = Create.RandomDouble();
        var weight = Create.RandomDouble();
        var maxPrice = Create.RandomDecimal();
        
        var command = CalculationDeliveryPriceCommandFaker.Generate()
            .WithUserId(userId);
        
        var builder = new CalculateDeliveryPriceHandlerBuilder();
        builder.CalculationService
            .SetupCalculationPriceByWeight(weight, maxPrice)
            .SetupCalculationPriceByVolume(volume, maxPrice - 0.001m);
        
        var handler = builder.Build();
        
        // Act
        var result = await handler.Handle(command, default);
        
        // Assert
        result.Price.Should().Be(maxPrice);
    }

    [Fact]
    public async Task Handle_ThrowsWhenNoGoods()
    {
        // Arrange
        var command = CalculationDeliveryPriceCommandFaker.Generate()
            .WithGoods(Array.Empty<GoodModel>());
        
        var builder = new CalculateDeliveryPriceHandlerBuilder();
        var handler = builder.Build();
        
        // Act
        var act = () => handler.Handle(command, default);
        
        // Assert
        await Assert.ThrowsAsync<GoodsNotFoundException>(act);
    }
}