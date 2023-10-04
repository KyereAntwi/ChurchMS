using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;
using SVoting.Application.Exceptions;

namespace COPDistrictMS.Application.Features.Members.Queries.GetMemberDetails;

public record GetMemberDetailsQuery(
    Guid MemberId
    ) : IRequest<BaseResponse>;

public class GetMemberDetailsQueryHandler : IRequestHandler<GetMemberDetailsQuery, BaseResponse>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetMemberDetailsQueryHandler(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(GetMemberDetailsQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetFullDetailsAsync( request.MemberId );

        if (member == null)
            throw new NotFoundException($"Specified member with Id {request.MemberId} was not found", typeof(Member));

        var memberDto = _mapper.Map<MemberDto>( member );

        if(member.Assembly is not null)
            memberDto.Address = _mapper.Map<AddressDto>( member.Assembly );

        if (member.OfficerType is not null)
            memberDto.OfficerType = member.OfficerType.Office;

        var response = new BaseResponse();

        response.Success = true;
        response.Data = memberDto;
        return response;
    }
}
