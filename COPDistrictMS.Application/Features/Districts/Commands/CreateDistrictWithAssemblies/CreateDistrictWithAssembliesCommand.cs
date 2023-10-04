using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateDistrictWithAssembliesCommandHandler(IAsyncRepository<District> asyncRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
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

        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var district = _mapper.Map<District>(request);
        district.CreatedAt = DateTime.UtcNow;
        district.UpdatedAt = DateTime.UtcNow;
        district.CreatedBy = userId;

        foreach(var assembly in request.AssemblyDtos!)
        {
            var newAssembly = _mapper.Map<Assembly>(assembly);
            newAssembly.CreatedBy = userId;
            newAssembly.CreatedAt = DateTime.UtcNow;
            newAssembly.UpdatedAt = DateTime.UtcNow;

            district.Assemblies.Add(newAssembly);
        }

        var newDistrict = await _asyncRepository.AddAsync(district);

        response.Success = true;
        response.Message = "Operation to create district with assemblies was successful";
        response.Data = newDistrict.Id;

        return response;
    }
}
