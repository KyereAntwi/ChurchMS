using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Offerings.Queries.GetMinistryOfferings;

public record GetMinistryOfferingsQuery(
    Guid DistrictId,
    int Page,
    int Size,
    string Ministry
    ) : IRequest<BaseResponse>;

public class GetMinistryOfferingQueryHandler : IRequestHandler<GetMinistryOfferingsQuery, BaseResponse>
{
    private readonly IDistrictRepository _districtRepository;
    private readonly IMapper _mapper;

    public GetMinistryOfferingQueryHandler(IDistrictRepository districtRepository, IMapper mapper)
    {
        _districtRepository = districtRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseResponse> Handle(GetMinistryOfferingsQuery request, CancellationToken cancellationToken)
    {
        var district = await _districtRepository.GetByIdAsync(request.DistrictId);

        if (district is null)
            throw new NotFoundException($"Specified district with id {request}", nameof(Assembly));

        var list = 
            await _districtRepository.GetDistrictOfferings(request.DistrictId, request.Ministry, request.Page, request.Size);

        var paggedList = new PagedListVm<MinistryOfferingDto>
        {
            Page = request.Page,
            Size = request.Size,
            PrevPage = request.Page > 1,
            NextPage = request.Size < list.Item1,
            ListItems = _mapper.Map<List<MinistryOfferingDto>>(list.Item2)
        };
        
        return new BaseResponse()
        {
            Success = true,
            Data = paggedList
        };
    }
}