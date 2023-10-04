using COPDistrictMS.Domain.Entities;
using FluentValidation;

namespace COPDistrictMS.Application.Features.Districts.Commands.UpdateDistrict;

public class UpdateDistrictCommandValidator : AbstractValidator<UpdateDistrictCommand>
{
    private readonly IAsyncRepository<District> _asyncRepository;

    public UpdateDistrictCommandValidator(IAsyncRepository<District> asyncRepository)
    {
        _asyncRepository = asyncRepository;

        RuleFor(d => d.Title)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(d => d.Area)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(d => d.DistrictPastor)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(d => d)
            .MustAsync(DistrictExists).WithMessage("Specified district does not exist");
    }

    public async Task<bool> DistrictExists(UpdateDistrictCommand command, CancellationToken token)
    {
        var district = await _asyncRepository.GetByIdAsync(command.Id);
        return district == null;
    }
}
