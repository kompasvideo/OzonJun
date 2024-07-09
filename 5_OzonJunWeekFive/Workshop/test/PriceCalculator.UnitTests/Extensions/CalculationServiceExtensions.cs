using Moq;

namespace PriceCalculator.UnitTests.Extensions;

public static class CalculationServiceExtensions
{
    public static Mock<ICalculationService> SetupSaveCalculation(
        this Mock<ICalculationService> service,
        long id)
    {
        service.Setup(p =>
                p.SaveCalculation(It.IsAny<SaveCalculationModel>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync();
        
        return service;
    }
    
    public static Mock<ICalculationService> SetupCalculationPriceByVolume(
        this Mock<ICalculationService> service,
        double volume,
        decimal price)
    {
        service.Setup(p =>
                p.CalculatePriceByVolume(It.IsAny<GoodModel[]>(),
                    out volume))
            .Returns(price);
        
        return service;
    }
    
    public static Mock<ICalculationService> SetupCalculatePriceByWeight(
        this Mock<ICalculationService> service,
        double weight,
        decimal price)
    {
        service.Setup(p =>
                p.CalculatePriceByVolume(It.IsAny<GoodModel[]>(),
                    out weight))
            .Returns(price);
        
        return service;
    }
    
    public static Mock<ICalculationService> SetupQueryCalculations(
        this Mock<ICalculationService> service,
        QueryCalculationModel[] model)
    {
        service.Setup(p =>
                p.QueryCalculations(It.IsAny<QueryCalculationFilter>(),
                    It.IaAny<CancellationToken>()))
            .ReturnsAsync(model);
        
        return service;
    }
}