using COPDistrictMS.Domain.Entities;
using FluentValidation;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.UpdateAssembly;

public class UpdateAssemblyCommandValidator : AbstractValidator<UpdateAssemblyCommand>
{
    private readonly IAsyncRepository<Assembly> _asyncRepository;

    public UpdateAssemblyCommandValidator(IAsyncRepository<Assembly> asyncRepository)
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
            .NotNull();

        RuleFor(a => a)
            .MustAsync(AssemblyExist).WithMessage("Specified assembly was not found");
    }

    public async Task<bool> AssemblyExist(UpdateAssemblyCommand command, CancellationToken token)
    {
        return await _asyncRepository.GetByIdAsync(command.Id) != null;
    }
}
