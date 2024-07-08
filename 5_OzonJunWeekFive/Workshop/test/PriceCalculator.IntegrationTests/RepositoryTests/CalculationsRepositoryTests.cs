using FluentAssertions;
using PriceCalculator.Dal.Models;
using PriceCalculator.Dal.Repositories.Interfaces;
using PriceCalculator.IntegrationTests.Fixtures;
using TestingInfrastructure.Creators;
using TestingInfrastructure.Fakers;

namespace PriceCalculator.IntegrationTests;

public class CalculationsRepositoryTests
{
    private readonly double _requiredDoublePrecision = 0.00001d;
    private readonly decimal _requiredDecimalPrecision = 0.00001m;
    private readonly TimeSpan _requiredDateTimePrecision = TimeSpan.FromMicroseconds(100);
    
    private readonly ICalculationsRepository _calculationsRepository;

    public CalculationsRepositoryTests(TestFixture fixture)
    {
        _calculationsRepository = fixture.CalculationsRepository;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public async Task Add_Calculations_Success(int count)
    {
        // Arrange
        var userId = Create.RandomId();
        var now = DateTimeOffset.UtcNow;
        
        var calculations = CalculationEntityV1Faker.Generate(count)
            .Select(x => x.WithUserId(userId)
                .WithAt(now))
            .ToArray();

        // Act
        var calculationIds = await _calculationsRepository.Add(calculations, default);
        
        // Assert
        calculationIds.Should().HaveCount(count);
        calculationIds.Should().OnlyContain(x => x > 0);
    }
    
    [Fact]
    public async Task Query_SingleCalculation_Success()
    {
        // Arrange
        var userId = Create.RandomId();
        var now = DateTimeOffset.UtcNow;
        
        var calculations = CalculationEntityV1Faker.Generate()
            .Select(x => x.WithUserId(userId)
                .WithAt(now))
            .ToArray();
        var expected = calculations.Single();
        
        var calculationId  = (await  _calculationsRepository.Add(calculations, default))
            .Single();
        
        // Act
        var foundCalculations = await _calculationsRepository.Query(
            new CalculationHistoryQueryModel(userId,1, 0), default);
        
        // Assert
        foundCalculations.Should().HaveCount(1);
        var calculation = foundCalculations.Single();
        
        calculation.Id.Should().Be(calculationId);
        calculation.UserId.Should().Be(expected.UserId);
        calculation.At.Should().BeCloseTo(expected.At, _requiredDateTimePrecision);
        calculation.Price.Should().BeApproximately(expected.Price, _requiredDecimalPrecision);
        calculation.GoodIds.Should().BeEquivalentTo(expected.GoodIds);
        calculation.TotalVolume.Should().BeApproximately(expected.TotalVolume,  _requiredDoublePrecision);
        calculation.TotalWeight.Should().BeApproximately(expected.TotalWeight,  _requiredDoublePrecision);
    }
}