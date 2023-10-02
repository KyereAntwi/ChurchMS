using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Districts.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;
using SVoting.Application.Exceptions;

namespace COPDistrictMS.Application.Features.Districts.Queries.GetSingle;

public record GetSingleDistrictQuery(Guid DistrictId) : IRequest<BaseResponse>;

public class GetSingleDistrictQueryHandler : IRequestHandler<GetSingleDistrictQuery, BaseResponse>
{
    private readonly IDistrictRepository _districtRepository;
    private readonly IMapper _mapper;

    public GetSingleDistrictQueryHandler(IDistrictRepository districtRepository, IMapper mapper)
    {
        _districtRepository  = districtRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(GetSingleDistrictQuery request, CancellationToken cancellationToken)
    {
        var district = await _districtRepository.GetByIdAsync(request.DistrictId);

        if (district == null)
        {
            throw new NotFoundException($"Specified district with Id {request.DistrictId} cannot be found", typeof(District));
        }

        var dto = _mapper.Map<DistrictDto>(district);

        dto.LocalAssembliesIds = await _districtRepository.GetDistrictAssemblyIds(request.DistrictId); 

        return new BaseResponse { Success = true, Data = dto };
    }
}

