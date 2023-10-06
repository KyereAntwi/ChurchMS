using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.Assemblies.Queries.GetManagedAssemblies;

public record GetManagedAssembliesQuery : IRequest<BaseResponse>;

public class GetManagedAssembliesQueryHandler : IRequestHandler<GetManagedAssembliesQuery, BaseResponse>
{
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetManagedAssembliesQueryHandler(IAssemblyRepository assemblyRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _assemblyRepository = assemblyRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse> Handle(GetManagedAssembliesQuery request, CancellationToken cancellationToken)
    {
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var list = await _assemblyRepository.GetManagedAssemblies(userId);

        var response = new BaseResponse
        {
            Success = true,
            Data = _mapper.Map<IEnumerable<AssemblyDto>>(list)
        };

        return response;

    }
}
