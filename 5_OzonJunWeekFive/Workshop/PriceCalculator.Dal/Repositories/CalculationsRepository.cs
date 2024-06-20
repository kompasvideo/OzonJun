using Dapper;
using PriceCalculator.Dal.Entities;
using PriceCalculator.Dal.Models;
using PriceCalculator.Dal.Repositories.Interfaces;
using PriceCalculator.Dal.Settings;

namespace PriceCalculator.Dal.Repositories;

public class CalculationsRepository :BaseRepository, ICalculationsRepository
{
    
    protected CalculationsRepository(DalOptions dalSettings) : base(dalSettings)
    {
    }

    public async Task<long[]> Add(
        CalculationEntityV1[] entityV1, 
        CancellationToken token)
    {
        const string sqlQuery =@"
INSERT INTO calculations(user_id, good_ids, total_volume, total_weight, price, at)
SELECT user_id, good_ids, total_volume, total_weight, price, at
    FROM UNNEST(@Calculations)
returnning id
";

        var sqlQueryParams = new
        {
            Calculations = entityV1
        };
        
        await using var connection = await GetAndOpenConnection();
        var ids = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery, 
                sqlQueryParams, 
                cancellationToken: token));
        
        return ids.ToArray();
    }

    public async Task<CalculationEntityV1[]> Query(
        CalculationHistoryQueryModel query, 
        CancellationToken token)
    {
        const string sqlQuery  = @"
SELECT    id
        , user_id
        , goods_ids
        , total_volume
        , total_weight
        , price
        , at
FROM calculations
WHERE user_id = @UserId
order by at desc
";
        
        var sqlQueryParams  = new
        {
            UserId = query.UserId,
            Limit = query.Limit,
            Offset  = query.Offset
        };
        
        await using var connection  = await GetAndOpenConnection();
        var calculations = await connection.QueryAsync<CalculationEntityV1>(
            new CommandDefinition(
                sqlQuery, 
                sqlQueryParams, 
                cancellationToken: token));
        
        return calculations.ToArray();
    }
}