using FluentValidation;
using PriceCalculator.Api.Requests;

namespace PriceCalculator.Api.Validators;

public class GetHistoryRequestValidator : AbstractValidator<GetHistoryRequest>
{
    public GetHistoryRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);
        RuleFor(x => x.Take)
            .GreaterThan(0);
    }
}