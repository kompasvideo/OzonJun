using Microsoft.AspNetCore.Http.HttpResults;
using PriceCalculator.Dal.Repositories.Interfaces;
using PriceCalculator.IntegrationTests.Fixtures;
using TestingInfrastructure.Creators;

namespace PriceCalculator.IntegrationTests;

public class GoodsRepositoryTests
{
    private readonly double _requiredDoublePrecision = 0.00001d;
    private readonly IGoodsRepository _goodsRepository;

    public GoodsRepositoryTests(TestFixture fixture)
    {
        _goodsRepository = fixture.GoodsRepository;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public async Task Add_Goods_Success(int count)
    {
        // Arrange
        var userId = Create.RandomId();
        
        var goods = GoodEntityV1Faker.Generate(count)
            .Select(x => x.WithUserId(userId))
            .ToArray();
        
        // Act
        var goodIds  = await _goodsRepository.Add(goods, default);
        
        // Assert
        goodIds.Should().HaveCount(count);
        goodIds.Should().OnlyContain(x => x > 0);
    }

    [Fact]
    public async Task Query_Goods_Success()
    {
        
    }
    
    [Fact]
    public async Task Query_SingleGood_Success()
    {
        
    }
    
    [Fact]
    public async Task Query_Goods_ReturnsEmpty_WhenForWrongUser()
    {
        
    }
}