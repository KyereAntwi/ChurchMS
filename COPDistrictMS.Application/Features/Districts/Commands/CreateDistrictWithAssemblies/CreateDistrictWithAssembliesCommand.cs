using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Districts.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Districts.Commands.CreateDistrictWithAssemblies;

public record CreateDistrictWithAssembliesCommand(
    string Title,
    string Area,
    string DistrictPastor,
    List<CreateAssemblyDto>? AssemblyDtos
    ) : IRequest<BaseResponse>;

public class CreateDistrictWithAssembliesCommandHandler : IRequestHandler<CreateDistrictWithAssembliesCommand, BaseResponse>
{
    private readonly IAsyncRepository<District> _asyncRepository;
    private readonly IMapper _mapper;

    public CreateDistrictWithAssembliesCommandHandler(IAsyncRepository<District> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(CreateDistrictWithAssembliesCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validation = new CreateDistrictWithAssembliesCommandValidator();
        var validationErrors = await validation.ValidateAsync(request, cancellationToken);

        if(validationErrors.Errors.Any())
        {
            response.Success = false;
            response.Message = "Operation to create district with assemblies failed";
            response.ValidationErrors = new List<string>();

            foreach (var error in validationErrors.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        var district = _mapper.Map<District>(request);

        foreach(var assembly in request.AssemblyDtos!)
        {
            district.Assemblies.Add(_mapper.Map<Assembly>(assembly));
        }

        var newDistrict = await _asyncRepository.AddAsync(district);

        response.Success = true;
        response.Message = "Operation to create district with assemblies was successful";
        response.Data = newDistrict.Id;

        return response;
    }
}
