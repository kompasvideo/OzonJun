using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Api.Requests;
using PriceCalculator.Api.Responses;

namespace PriceCalculator.Api.Controllers.V1;

[ApiController]
[Route("/v1/delivery-prices")]
public class DeliveryPricesController
{
    /// <summary>
    /// Метод расчёта стоимости доставки на основе объема товара
    /// и цен товара. Окончательная стоимость принимается как наибольшая.
    /// </summary>
    /// <returns></returns>
    [HttpPost("valculate")]
    public async Task<CalculateResponse> Calculate(
        CalculateRequest request,
        CancellationToken ct)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Метод получения истории вычисления
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost("get-history")]
    public async Task<GetHistoryResponse[]> History(
        GetHistoryRequest request,
        CancellationToken ct)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
    
}