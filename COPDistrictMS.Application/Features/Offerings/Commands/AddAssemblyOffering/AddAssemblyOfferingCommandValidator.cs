using COPDistrictMS.Application.Contracts.Persistence;
using FluentValidation;

namespace COPDistrictMS.Application.Features.Offerings.Commands.AddAssemblyOffering;

public class AddAssemblyOfferingCommandValidator : AbstractValidator<AddAssemblyOfferingCommand>
{
    public AddAssemblyOfferingCommandValidator()
    {
        RuleFor(o => o.OfferingType)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .NotNull();

        RuleFor(o => o.Amount)
            .GreaterThan(0).WithMessage("{PropertyName} must be more than 0.0");
    }
}