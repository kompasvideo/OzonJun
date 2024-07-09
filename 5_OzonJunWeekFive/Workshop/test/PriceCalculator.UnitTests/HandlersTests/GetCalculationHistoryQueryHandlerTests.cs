using PriceCalculator.UnitTests.Fakers;
using TestingInfrastructure.Creators;

namespace PriceCalculator.UnitTests.HandlersTests;

public class GetCalculationHistoryQueryHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCalls()
    {
        // Arrange
        var userId = Create.RandomId();
        
        var command = GetCalculationHistoryQueryFaker.Generate()
            .WithUserId(userId);
        
        var queryModels = QueryCalculationModelFaker.Generate(15)
            .Select(x => x.WithUserId(userId))
                .ToArray();

        var filter = QueryCalculationFilterFaker.Generate()
            .WithUserId(userId)
            .WithLimit(command.Limit)
            .WithOffset(command.Skip);
        
        var builder
        // Act
        await handler.Handle(null!, default);

        // Assert
    }
}