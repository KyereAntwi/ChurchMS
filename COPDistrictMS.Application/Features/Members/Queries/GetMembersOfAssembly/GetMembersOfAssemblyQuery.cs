using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Domain.Entities;
using MediatR;
using SVoting.Application.Exceptions;

namespace COPDistrictMS.Application.Features.Members.Queries.GetMembersOfAssembly;

public record GetMembersOfAssemblyQuery(
    Guid AssemblyId,
    int Page,
    int Size
    ) : IRequest<BaseResponse>;

public class GetMembersOfAssemblyQueryHandler : IRequestHandler<GetMembersOfAssemblyQuery, BaseResponse>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IMapper _mapper;

    public GetMembersOfAssemblyQueryHandler(IMemberRepository memberRepository, IAssemblyRepository assemblyRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _assemblyRepository = assemblyRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(GetMembersOfAssemblyQuery request, CancellationToken cancellationToken)
    {
        var assembly = await _assemblyRepository.GetByIdAsync( request.AssemblyId );
        if (assembly == null)
            throw new NotFoundException("", typeof(Assembly));

        var pagedMembersList = await _memberRepository.GetMembersByAssembly(request.AssemblyId, request.Page, request.Size);

        var membersDto = _mapper.Map<List<MemberDto>>(pagedMembersList.Item1);

        var listDto = new PagedListVm<MemberDto>
        {
            ListItems = membersDto,
            Page = request.Page,
            Size = request.Size,
            Count = pagedMembersList.Item2,
            PrevPage = request.Page > 1,
            NextPage = request.Size < pagedMembersList.Item2
        };

        var response = new BaseResponse()
        {
            Success = true,
            Data = listDto
        };

        return response;
    }
}
