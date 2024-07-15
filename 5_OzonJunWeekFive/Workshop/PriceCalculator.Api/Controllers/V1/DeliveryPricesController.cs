using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Api.Requests;
using PriceCalculator.Api.Responses;
using PriceCalculator.Bll.Commands;
using PriceCalculator.Bll.Queries;

namespace PriceCalculator.Api.Controllers.V1;

[ApiController]
[Route("/v1/delivery-prices")]
public class DeliveryPricesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeliveryPricesController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Метод расчёта стоимости доставки на основе объема товара
    /// или веса товара. Окончательная стоимость принимается как наибольшая.
    /// </summary>
    /// <returns></returns>
    [HttpPost("valculate")]
    public async Task<CalculateResponse> Calculate(
        CalculateRequest request,
        CancellationToken ct)
    {
        var command = new CalculateDeliveryPriceCommand(
            request.UserId,
            request.Goods
                .Select(x => new GoodModel(
                    x.Height,
                    x.Length,
                    x.Width,
                    x.Weight))
                .ToArray());
        var result = await _mediator.Send(command, ct);
        return new CalculateResponse(
            result.CalculationId,
            result.Price);
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
       var query = new GetCalculationHistoryQuery(
           request.UserId,
           request.Take,
           request.Skip);
       var result = await _mediator.Send(query, ct);

       return result.Items
           .Select(x => new GetHistoryResponse(
               new GetHistoryResponse.CargoResponse(
                   x.Volume,
                   x.Weight,
                   x.GoodIds),
               x.Price))
           .ToArray();
    }
}