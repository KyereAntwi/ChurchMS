using FluentValidation;

namespace COPDistrictMS.Application.Features.Districts.Commands.CreateDistrictWithAssemblies;

public class CreateDistrictWithAssembliesCommandValidator : AbstractValidator<CreateDistrictWithAssembliesCommand>
{
    public CreateDistrictWithAssembliesCommandValidator()
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

        RuleFor(d => d.AssemblyDtos)
            .NotNull();

        RuleForEach(d => d.AssemblyDtos)
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
    }
}
