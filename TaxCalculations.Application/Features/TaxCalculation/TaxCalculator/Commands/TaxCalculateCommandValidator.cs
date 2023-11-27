using FluentValidation;

namespace TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;

public class TaxCalculateCommandValidator : AbstractValidator<TaxCalculateCommand>
{

    public TaxCalculateCommandValidator()
    {
        RuleFor(o => o.AnnualIncome)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();


        RuleFor(o => o.PostalCode)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(4).WithMessage("{PropertyName} must not exceed 4 characters.")
            .MinimumLength(4).WithMessage("{PropertyName} must not be less than 4 characters.");
    }
}
