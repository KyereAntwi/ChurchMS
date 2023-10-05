using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;
using COPDistrictMS.Application.Exceptions;

namespace COPDistrictMS.Application.Features.Members.Commands.DeleteMember;

public record DeleteMemberCommand(Guid MemberId) : IRequest<BaseResponse>;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, BaseResponse>
{
    private readonly IAsyncRepository<Member> _asyncRepository;

    public DeleteMemberCommandHandler(IAsyncRepository<Member> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<BaseResponse> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var member = await _asyncRepository.GetByIdAsync(request.MemberId);
        if (member is null)
            throw new NotFoundException($"Specified member with Id {request.MemberId} was not found", typeof(Member));

        await _asyncRepository.DeleteAsync(member);

        response.Success = true;
        return response;
    }
}
