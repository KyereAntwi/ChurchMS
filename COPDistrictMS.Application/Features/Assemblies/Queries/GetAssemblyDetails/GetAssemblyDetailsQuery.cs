using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Assemblies.Queries.GetAssemblyDetails;

public record GetAssemblyDetailsQuery(
    Guid AssemblyId
    ) : IRequest<BaseResponse>;

public class GetAssemblyDetailsQueryHandler : IRequestHandler<GetAssemblyDetailsQuery, BaseResponse>
{
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IMapper _mapper;

    public GetAssemblyDetailsQueryHandler(IAssemblyRepository assemblyRepository, IMapper mapper)
    {
        _assemblyRepository = assemblyRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(GetAssemblyDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var assembly = await _assemblyRepository.GetAssemblyWithDistrict(request.AssemblyId);

        if (assembly == null)
            throw new NotFoundException("", typeof(Assembly));

        var assemblyDto = _mapper.Map<AssemblyDto>(assembly);

        if (assembly.District is not null)
        {
            assemblyDto.District = _mapper.Map<DistrictDto>(assembly.District);
        }

        if (assembly.Managers.Count > 0)
            assemblyDto.ManagersUsernames = assembly.Managers;

        if (assembly.PresidingOfficer is not null)
            assemblyDto.PresidingLeader = assembly.PresidingOfficer.Id;

        response.Data = assemblyDto;

        return response;
    }
}
