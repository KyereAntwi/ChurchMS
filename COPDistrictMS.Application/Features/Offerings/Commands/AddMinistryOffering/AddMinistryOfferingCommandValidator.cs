using FluentValidation;

namespace COPDistrictMS.Application.Features.Offerings.Commands.AddMinistryOffering;

public class AddMinistryOfferingCommandValidator : AbstractValidator<AddMinistryOfferingCommand>
{
    public AddMinistryOfferingCommandValidator()
    {
        RuleFor(o => o.Ministry)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .NotNull();

        RuleFor(o => o.Amount)
            .GreaterThan(0).WithMessage("{PropertyName} must be more than 0.0");
    }
}