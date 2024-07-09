using Moq;
using PriceCalculator.Dal.Repositories.Interfaces;

namespace PriceCalculator.UnitTests.Builders;

public class CalculationServiceBuilder
{
    public Mock<ICalculationRepository> CalculationRepository;
    public Mock<IGoodsRepository> GoodsRepository;

    public CalculationServiceBuilder()
    {
        CalculationRepository = new Mock<ICalculationRepository>();
        GoodsRepository = new Mock<IGoodsRepository>();
    }

    public CalculationServiceStub Build()
    {
        return new CalculationServiceStub(
            CalculationRepository, 
            GoodsRepository);
    }
}