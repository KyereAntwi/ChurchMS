using FluentValidation;

namespace COPDistrictMS.Application.Features.Districts.Commands.CreateADistrict;

public class CreateADistrictCommandValidator : AbstractValidator<CreateADistrictCommand>
{
    public CreateADistrictCommandValidator()
    {
        RuleFor(d => d.Title)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(d => d.Area)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(d => d.DistrictPastor)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
    }
}
