using FluentValidation;
using PriceCalculator.Api.Requests;

namespace PriceCalculator.Api.Validators;

public class CalculateRequestValidator : AbstractValidator<CalculateRequest>
{
    public CalculateRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);
        RuleFor(x => x.Goods)
            .NotEmpty();
        RuleForEach(x  => x.Goods)
            .SetValidator(new CalculateRequestGoodPropertiesValidator());
    }
}