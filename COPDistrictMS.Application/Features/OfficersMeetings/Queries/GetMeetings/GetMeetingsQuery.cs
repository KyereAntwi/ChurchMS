using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.OfficersMeetings.Queries.GetMeetings;

public record GetMeetingsQuery(
    Guid DistrictId,
    DateTime CreatedAt,
    string PastorInCharge,
    int Page,
    int Size
    ) : IRequest<BaseResponse>;

public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, BaseResponse>
{
    private readonly IOfficersMeetingRepository _officersMeetingRepository;
    private readonly IAsyncRepository<District> _asyncRepository;
    private readonly IMapper _mapper;

    public GetMeetingsQueryHandler(IOfficersMeetingRepository officersMeetingRepository, 
        IAsyncRepository<District> asyncRepository,
        IMapper mapper)
    {
        _officersMeetingRepository = officersMeetingRepository;
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseResponse> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
    {
        var district = await _asyncRepository.GetByIdAsync(request.DistrictId);

        if (district is null)
            throw new NotFoundException($"Specified District with ID {request.DistrictId}!", nameof(District));
        
        var result = await _officersMeetingRepository.GetMeetingsForDistrict(request.DistrictId, request.CreatedAt,
            request.PastorInCharge, request.Page, request.Size);

        var pagedList = new PagedListVm<OfficersMeetingDto>
        {
            Page = request.Page,
            Size = request.Size,
            NextPage = request.Size < result.Item1,
            PrevPage = request.Page > 1,
            ListItems = _mapper.Map<ICollection<OfficersMeetingDto>>(result.Item2)
        };

        var response = new BaseResponse
        {
            Success = true,
            Data = pagedList
        };

        return response;
    }
}