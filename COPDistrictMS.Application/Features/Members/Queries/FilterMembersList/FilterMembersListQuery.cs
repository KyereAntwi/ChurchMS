using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Features.Dtos;
using MediatR;

namespace COPDistrictMS.Application.Features.Members.Queries.FilterMembersList;

public record FilterMembersListQuery(
    Guid DistrictId,
    string NameString,
    string Gender,
    Guid AssemblyId,
    DateOnly DateOfBirth,
    int MonthOfBirth,
    int YearOfBirth,
    int Page,
    int Size
    ) : IRequest<BaseResponse>;

public class FilterMembersListQueryHandler : IRequestHandler<FilterMembersListQuery, BaseResponse>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public FilterMembersListQueryHandler(
        IMemberRepository memberRepository, 
        IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(FilterMembersListQuery request, CancellationToken cancellationToken)
    {
        var filteredMembersList = await _memberRepository.FilterMembersByDistrict(
            request.AssemblyId,
            request.DistrictId,
            request.NameString,
            request.Gender,
            request.DateOfBirth,
            request.YearOfBirth,
            request.MonthOfBirth,
            request.Page,
            request.Size
            );

        var membersDto = _mapper.Map<List<MemberDto>>(filteredMembersList.Item1);

        var listDto = new PagedListVm<MemberDto>
        {
            ListItems = membersDto,
            Page = request.Page,
            Size = request.Size,
            Count = filteredMembersList.Item2,
            PrevPage = request.Page > 1,
            NextPage = request.Size < filteredMembersList.Item2
        };

        var response = new BaseResponse()
        {
            Success = true,
            Data = listDto
        };

        return response;
    }
}
