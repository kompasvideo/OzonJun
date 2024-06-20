namespace PriceCalculator.Dal.Models;

public record CalculationHistoryQueryModel(
    long UserId,
    int Limit,
    int Offset);
    
