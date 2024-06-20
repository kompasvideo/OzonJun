using Dapper;
using Microsoft.Extensions.Options;
using PriceCalculator.Dal.Entities;
using PriceCalculator.Dal.Models;
using PriceCalculator.Dal.Repositories.Interfaces;
using PriceCalculator.Dal.Settings;

namespace PriceCalculator.Dal.Repositories;

public class GoodsRepository : BaseRepository,IGoodsRepository
{
    public GoodsRepository(
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<long[]> Add(
        GoodEntityV1[] goods,
        CancellationToken token)
    {
        const string sqlQuery =@"
INSERT INTO Goods(user_id, width, height, length, weight)
SELECT user_id, width, height, length, weight
    FROM UNNEST(@Goods)
returnning id
";

        var sqlQueryParams = new
        {
            Goods = goods
        };
        
        await using var connection = await GetAndOpenConnection();
        var ids = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery, 
                sqlQueryParams, 
                cancellationToken: token));
        
        return ids.ToArray();
    }

    public async Task<GoodEntityV1[]> Query(
        long userId,
        CancellationToken token
    )
    {
        const string sqlQuery  = @"
SELECT    id
        , user_id
        , width
        , height
        , length
        , weight
FROM Goods
WHERE user_id = @UserId;
";
        
        var sqlQueryParams  = new
        {
            UserId = userId
        };
        
        await using var connection  = await GetAndOpenConnection();
        var goods = await connection.QueryAsync<GoodEntityV1>(
        new CommandDefinition(
            sqlQuery, 
            sqlQueryParams, 
            cancellationToken: token));
        
        return goods.ToArray();
    }
}