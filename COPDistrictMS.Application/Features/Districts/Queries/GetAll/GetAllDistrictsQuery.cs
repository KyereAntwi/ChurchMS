using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.Districts.Queries.GetAll;

public record GetAllDistrictsQuery() : IRequest<BaseResponse>;

public class GetAllDistrictsQueryHandler : IRequestHandler<GetAllDistrictsQuery, BaseResponse>
{
    private readonly IDistrictRepository _asyncRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllDistrictsQueryHandler(IDistrictRepository asyncRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
    {
        List<DistrictDto> districtDtos = new List<DistrictDto>();

        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var list = await _asyncRepository.GetAllOwned(userId);

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
