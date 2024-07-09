using System.Transactions;
using Moq;
using PriceCalculator.Dal.Entities;
using PriceCalculator.Dal.Models;

namespace PriceCalculator.UnitTests.Extensions;

public static class CalculationRepositoryExtensions
{
    public static Mock<CalculationRepository> SetupAddCalculations(
        this Mock<ICalculationRepository repository,
        long[] ids)
    {
        repository.Setup(p => 
            p.Add(It.Any<CalculationEntityV1[]>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(ids);
        
        return repository;
    }

    public static Mock<ICalculationRepository> SetupCreateTransactionScope(
        this Mock<ICalculationRepository> repository)
    {
        repository.Setup(p => 
            p.CreateTransactionScope(It.IsAny<IsolationLevel>()))
            .Returns(new TransactionScope());
        
        return repository;
    }

    public static Mock<ICalculationRepository> SetupQueryCalculation(
        this Mock<ICalculationRepository> repository,
        CalculationEntityV1[] calculations)
    {
        repository.Setup(p =>
            p.Query(It.IsAny<CalculationHistoryQueryModel>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(calculations);
        
        return repository;
    }
    
    public static Mock<ICalculationRepository> VerifyQueryWasCalledOnce(
        this Mock<ICalculationRepository> repository,
        CalculationHistoryQueryModel query)
    {
        repository.Verify(p =>
                p.Query(
                    It.Is<CalculationHistoryQueryModel>(x => x == query),
                    It.IsAny<CancellationToken>()),
            Times.Once);
        
        return repository;
    }
    
    public static Mock<ICalculationRepository> VerifyCreateTransactionScopeWasCalledOnce(
        this Mock<ICalculationRepository> repository,
        IsolationLevel isolationLevel)
    {
        repository.Verify(p =>
                p.CreateTransactionScope(
                    It.Is<IsolationLevel>(x => x == isolationLevel)),
            Times.Once);
        
        return repository;
    }

}