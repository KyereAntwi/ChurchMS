using COPDistrictMS.Domain.Entities;
using FluentValidation;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.CreateMultipleAssemblies;

public class CreateMultipleAssembliesCommandValidator : AbstractValidator<CreateMultipleAssembliesCommand>
{
    private readonly IDistrictRepository _asyncRepository;

    public CreateMultipleAssembliesCommandValidator(IDistrictRepository asyncRepository)
    {
        _asyncRepository = asyncRepository;

        RuleFor(d => d.LoggedInUsername)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();

        RuleForEach(d => d.CreateAssemblyDtos)
            .ChildRules(a =>
            {
                a.RuleFor(assembly => assembly.Title)
                    .NotEmpty().WithMessage("{PropertyName} must not be empty")
                    .NotNull();

                a.RuleFor(assembly => assembly.Location)
                   .NotEmpty().WithMessage("{PropertyName} must not be empty")
                   .NotNull();

                a.RuleFor(assembly => assembly.YearEstablished).MinimumLength(4);
            });

        RuleFor(d => d)
            .MustAsync(DistrictExist).WithMessage("Specified district does not exist");
    }

    public async Task<bool> DistrictExist(CreateMultipleAssembliesCommand command, CancellationToken token)
    {
        return await _asyncRepository.GetByIdAsync(command.DistrictId) != null;
    }
}
