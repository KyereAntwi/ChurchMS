using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Dtos;
using MediatR;

namespace COPDistrictMS.Application.Features.Districts.Queries.GetAll;

public record GetAllDistrictsQuery() : IRequest<BaseResponse>;

public class GetAllDistrictsQueryHandler : IRequestHandler<GetAllDistrictsQuery, BaseResponse>
{
    private readonly IDistrictRepository _asyncRepository;
    private readonly IMapper _mapper;

    public GetAllDistrictsQueryHandler(IDistrictRepository asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
    {
        List<DistrictDto> districtDtos = new List<DistrictDto>();

        var list = await _asyncRepository.ListAllAsync();

        if (list.Any())
        {
            foreach (var district in list)
            {
                var districtDto = _mapper.Map<DistrictDto>(district);
                districtDto.LocalAssembliesIds = await _asyncRepository.GetDistrictAssemblyIds(district.Id);
                districtDtos.Add(districtDto);
            }
        }

        var response = new BaseResponse { Success = true, Data = districtDtos };
        return response;
    }
}
