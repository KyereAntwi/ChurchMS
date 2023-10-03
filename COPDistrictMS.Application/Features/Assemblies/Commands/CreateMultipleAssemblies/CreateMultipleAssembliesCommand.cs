using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.CreateMultipleAssemblies;

public record CreateMultipleAssembliesCommand(
    Guid DistrictId,
    string LoggedInUsername,
    List<CreateAssemblyDto> CreateAssemblyDtos
    ) : IRequest<BaseResponse>;

public class CreateMultipleAssembliesCommandHandler : IRequestHandler<CreateMultipleAssembliesCommand, BaseResponse>
{
    private readonly IDistrictRepository _asyncRepository;
    private readonly IMapper _mapper;

    public CreateMultipleAssembliesCommandHandler(IDistrictRepository asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(CreateMultipleAssembliesCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validation = new CreateMultipleAssembliesCommandValidator(_asyncRepository);
        var validationErrors = await validation.ValidateAsync(request, cancellationToken);

        if (validationErrors.Errors.Any())
        {
            response.Success = false;
            response.Message = "Operation to create assemblies failed";
            response.ValidationErrors = new List<string>();

            foreach (var error in validationErrors.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        var district = await _asyncRepository.GetDistrictWithAssembliesFull(request.DistrictId);

        foreach (var assembly in request.CreateAssemblyDtos)
        {
            district!.Assemblies.Add(_mapper.Map<Assembly>(assembly));
            await _asyncRepository.SaveAsync();
        }

        response.Success = true;
        response.Message = "Operation successful";
        response.Data = district!.Id;

        return response;
    }
}