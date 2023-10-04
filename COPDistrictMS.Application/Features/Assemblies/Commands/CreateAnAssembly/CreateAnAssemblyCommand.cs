using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.CreateAnAssembly;

public record CreateAnAssemblyCommand(
    string Title,
    string Location,
    string YearEstablished,
    Guid DistrictId
    ) : IRequest<BaseResponse>;

public class CreateAnAssemblyCommandHandler : IRequestHandler<CreateAnAssemblyCommand, BaseResponse>
{
    private readonly IAsyncRepository<Assembly> _asyncRepository;
    private readonly IAsyncRepository<District> _districtRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CreateAnAssemblyCommandHandler(
        IAsyncRepository<Assembly> asyncRepository, 
        IAsyncRepository<District> districtRepository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _districtRepository = districtRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(CreateAnAssemblyCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validation = new CreateAnAssemblyCommandValidator(_districtRepository);
        var validationErrors = await validation.ValidateAsync(request, cancellationToken);

        if(validationErrors.Errors.Any())
        {
            response.Success = false;
            response.Message = "Operation to create an assembly was not successful";
            response.ValidationErrors = new List<string>();

            foreach (var validationError in validationErrors.Errors)
                response.ValidationErrors.Add(validationError.ErrorMessage);

            return response;
        }

        response.Success = true;
        response.Message = "Operation was successful";

        var assembly = _mapper.Map<Assembly>(request);

        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        assembly.CreatedBy = userId;
        assembly.CreatedAt = DateTime.Now;
        assembly.UpdatedAt = DateTime.Now;

        var newAssembly = await _asyncRepository.AddAsync(assembly);

        response.Data = newAssembly.Id;

        return response;
    }
}
