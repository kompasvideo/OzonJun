namespace PriceCalculator.Api.Requests;

public record GetHistoryRequest(
    long UserId,
    int Take,
    int Skip);