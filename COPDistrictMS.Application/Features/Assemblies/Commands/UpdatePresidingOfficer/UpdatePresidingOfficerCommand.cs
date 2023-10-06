using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.UpdatePresidingOfficer;

public record UpdatePresidingOfficerCommand(
    Guid AssemblyId,
    Guid MemberId
    ) : IRequest<BaseResponse>;

public class UpdatePresidingOfficerCommandHandler : IRequestHandler<UpdatePresidingOfficerCommand, BaseResponse>
{
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IMemberRepository _memberRepository;

    public UpdatePresidingOfficerCommandHandler(IAssemblyRepository assemblyRepository, IMemberRepository memberRepository)
    {
        _assemblyRepository = assemblyRepository;
        _memberRepository = memberRepository;
    }

    public async Task<BaseResponse> Handle(UpdatePresidingOfficerCommand request, CancellationToken cancellationToken)
    {
        var assembly = await _assemblyRepository.GetByIdAsync(request.AssemblyId);

        if (assembly is null)
            throw new NotFoundException($"Specified assembly with id {request.AssemblyId} does not exist.", nameof(Assembly));

        var member = await _memberRepository.GetByIdAsync(request.MemberId);

        if(member is null)
            throw new NotFoundException($"Specified member with id {request.MemberId} does not exist.", nameof(Member));

        assembly.PresidingOfficer = new AssemblyPresiding()
        {
            Member = member,
        };

        await _assemblyRepository.SaveAsync();

        return new BaseResponse
        {
            Success = true,
            Message = "Operation successful"
        };
    }
}
