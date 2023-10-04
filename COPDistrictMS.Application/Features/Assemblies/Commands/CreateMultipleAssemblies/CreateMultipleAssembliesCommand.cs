using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.CreateMultipleAssemblies;

public record CreateMultipleAssembliesCommand(
    Guid DistrictId,
    List<CreateAssemblyDto> CreateAssemblyDtos
    ) : IRequest<BaseResponse>;

public class CreateMultipleAssembliesCommandHandler : IRequestHandler<CreateMultipleAssembliesCommand, BaseResponse>
{
    private readonly IDistrictRepository _asyncRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateMultipleAssembliesCommandHandler(IDistrictRepository asyncRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
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
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        foreach (var assembly in request.CreateAssemblyDtos)
        {
            var newAssembly = _mapper.Map<Assembly>(assembly);
            newAssembly.CreatedBy = userId;
            newAssembly.CreatedAt = DateTime.UtcNow;
            newAssembly.UpdatedAt = DateTime.UtcNow;

            district!.Assemblies.Add(newAssembly);
            await _asyncRepository.SaveAsync();
        }

        response.Success = true;
        response.Message = "Operation successful";
        response.Data = district!.Id;

        return response;
    }
}