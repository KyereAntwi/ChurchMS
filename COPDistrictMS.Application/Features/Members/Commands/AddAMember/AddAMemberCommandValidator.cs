using COPDistrictMS.Application.Contracts.Persistence;
using FluentValidation;

namespace COPDistrictMS.Application.Features.Members.Commands.AddAMember;

public class AddAMemberCommandValidator : AbstractValidator<AddAMemberCommand>
{
    private readonly IOfficerRepository _officerRepository;

    public AddAMemberCommandValidator(IOfficerRepository officerRepository)
    {
        _officerRepository = officerRepository;

        RuleFor(m => m.GivingName)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(m => m.DateOfBirth)
            .Must(BeAValidDate).WithMessage("Date of birth is required")
            .NotNull();

        RuleFor(m => m.Gender)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .MinimumLength(4).WithMessage("{PropertyName} must be at least 4 characters. Either Male or Female")
            .MaximumLength(6).WithMessage("{PropertyName} must be at most 6 characters. Either Male or Female")
            .NotNull();

        RuleFor(m => m.Residence)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(m => m.Telephone)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
    }

    private bool BeAValidDate(DateOnly only)
    {
        return !only.Equals(default(DateOnly));
    }
}
