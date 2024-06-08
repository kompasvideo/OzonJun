namespace PriceCalculator.Api.Requests;

/// <summary>
/// Товары, чью цену транспортировки нужно рассчитать
/// </summary>
public record CalculateRequest(
    long UserId,
    CalculateRequest.GoodProperties[] Goods)
{
    public record GoodProperties(
        double Height,
        double Length,
        double Width,
        double Weight);
}