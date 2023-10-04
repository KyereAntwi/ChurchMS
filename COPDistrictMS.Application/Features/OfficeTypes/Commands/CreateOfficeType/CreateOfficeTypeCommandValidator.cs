using COPDistrictMS.Application.Contracts.Persistence;
using FluentValidation;

namespace COPDistrictMS.Application.Features.OfficeTypes.Commands.CreateOfficeType;

public class CreateOfficeTypeCommandValidator : AbstractValidator<CreateOfficeTypeCommand>
{
    private readonly IOfficerRepository _officeRepository;

    public CreateOfficeTypeCommandValidator(IOfficerRepository officerRepository)
    {
        _officeRepository = officerRepository;

        RuleFor(o => o.Office)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(o => o)
            .MustAsync(AlreadyExist).WithMessage("Specified office type already exist");
    }

    private async Task<bool> AlreadyExist(CreateOfficeTypeCommand command, CancellationToken token)
    {
        var office = await _officeRepository.GetByType(command.Office);

        if (office == null)
            return true;
        return false;
    }
}
