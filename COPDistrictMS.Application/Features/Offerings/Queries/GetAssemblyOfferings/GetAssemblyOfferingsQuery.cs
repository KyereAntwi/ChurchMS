using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Offerings.Queries.GetAssemblyOfferings;

public record GetAssemblyOfferingsQuery(
    Guid AssemblyId,
    int Page,
    int Size,
    string OfferingType
    ) : IRequest<BaseResponse>;

public class GetAssemblyOfferingQueryHandler : IRequestHandler<GetAssemblyOfferingsQuery, BaseResponse>
{
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IMapper _mapper;

    public GetAssemblyOfferingQueryHandler(IAssemblyRepository assemblyRepository, IMapper mapper)
    {
        _assemblyRepository = assemblyRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseResponse> Handle(GetAssemblyOfferingsQuery request, CancellationToken cancellationToken)
    {
        var assembly = await _assemblyRepository.GetByIdAsync(request.AssemblyId);

        if (assembly is null)
            throw new NotFoundException($"Specified assembly with id {request.AssemblyId}", nameof(Assembly));

        var list = 
            await _assemblyRepository.GetAssemblyOfferings(request.AssemblyId, request.OfferingType, request.Page, request.Size);

        var paggedList = new PagedListVm<AssemblyOfferingDto>
        {
            Page = request.Page,
            Size = request.Size,
            PrevPage = request.Page > 1,
            NextPage = request.Size < list.Item1,
            ListItems = _mapper.Map<List<AssemblyOfferingDto>>(list.Item2)
        };
        
        return new BaseResponse()
        {
            Success = true,
            Data = paggedList
        };
    }
}