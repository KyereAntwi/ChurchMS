using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.OfficeTypes.Queries.GetAll;

public record GetAllQuery : IRequest<BaseResponse>;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, BaseResponse>
{
    private readonly IAsyncRepository<OfficerType> _asyncRepository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IAsyncRepository<OfficerType> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        IEnumerable<OfficeTypeDto> officeTypeDtos = new List<OfficeTypeDto>();

        var list = await _asyncRepository.ListAllAsync();
        
        if (list != null)
            officeTypeDtos = _mapper.Map<List<OfficeTypeDto>>(list);

        response.Data = officeTypeDtos;
        return response;
    }
}
