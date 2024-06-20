using PriceCalculator.Dal.Entities;
using PriceCalculator.Dal.Models;

namespace PriceCalculator.Dal.Repositories.Interfaces;

public interface ICalculationsRepository :IDbRepository
{
    Task<long[]> Add(
        CalculationEntityV1[] entityV1, 
        CancellationToken token);

    Task<CalculationEntityV1[]> Query(
        CalculationHistoryQueryModel query,
        CancellationToken token);
}