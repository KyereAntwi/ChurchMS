using COPDistrictMS.Domain.Entities;
using FluentValidation;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.CreateAnAssembly;

public class CreateAnAssemblyCommandValidator : AbstractValidator<CreateAnAssemblyCommand>
{
    private readonly IAsyncRepository<District> _asyncRepository;

    public CreateAnAssemblyCommandValidator(IAsyncRepository<District> asyncRepository)
    {
        _asyncRepository = asyncRepository;

        RuleFor(a => a.Title)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(a => a.Location)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(a => a.YearEstablished)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .MinimumLength(4)
            .NotNull();

        RuleFor(a => a.LoggedInUsername)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleFor(a => a)
            .MustAsync(DistrictExist).WithMessage("The specified district does not exist");
    }

    public async Task<bool> DistrictExist(CreateAnAssemblyCommand command, CancellationToken token)
    {
        return await _asyncRepository.GetByIdAsync(command.DistrictId) != null;
    }
}
