using Moq;
using PriceCalculator.Dal.Entities;
using PriceCalculator.Dal.Repositories.Interfaces;

namespace PriceCalculator.UnitTests.Extensions;

public static class GoodsRepositoryExtensions
{
    public static Mock<IGoodsRepository> SetupAddGoods(
        this Mock<IGoodsRepository> repository,
        long[] ids)
    {
        repository.Setup(p => 
                p.Add(It.IsAny<GoodEntityV1[]>(),
                It.IsAny<CalculationToken>()))
                    .ReturnsAsync(ids);

        return repository;
    }
    
    public static Mock<IGoodsRepository> SetupQueryGoods(
        this Mock<IGoodsRepository> repository,
        GoodEntityV1[] goods)
    {
        repository.Setup(p => 
            p.Query(It.IsAny<Long>(),
        It.IsAny<CalculationToken>()))
            .ReturnsAsync(goods);

        return repository;
    }
    
    public static void VerifyAddWasCalledOnce(
        this Mock<IGoodsRepository> repository,
        GoodEntityV1[] goods)
    {
        repository.Setup(p => 
            p.Add(It.IsAny<GoodEntityV1[]>()
        It.IsAny<CalculationToken>()))
            .ReturnsAsync(ids);

        return repository;
    }
}