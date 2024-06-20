using PriceCalculator.Dal.Entities;
using PriceCalculator.Dal.Models;

namespace PriceCalculator.Dal.Repositories.Interfaces;

public interface IGoodsRepository :IDbRepository
{
    Task<long[]> Add(
        GoodEntityV1[] goods, 
        CancellationToken token);

    Task<GoodEntityV1[]> Query(
        long userId,
        CancellationToken token);
}