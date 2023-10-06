using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Exceptions;
using MediatR;
using System.Reflection;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.AddManagersToAssembly;

public class AddManagersToAssemblyCommand : IRequest<BaseResponse>
{
    public Guid AssemblyId { get; set; }
    public List<string> ManageUsernames { get; set; } = new List<string>();
}

public class AddManagersToAssemblyCommandHandler : IRequestHandler<AddManagersToAssemblyCommand, BaseResponse>
{
    private readonly IAssemblyRepository _assemblyRepository;

    public AddManagersToAssemblyCommandHandler(IAssemblyRepository assemblyRepository)
    {
        _assemblyRepository = assemblyRepository;
    }

    public async Task<BaseResponse> Handle(AddManagersToAssemblyCommand request, CancellationToken cancellationToken)
    {
        var assembly = await _assemblyRepository.GetByIdAsync(request.AssemblyId);

        if (assembly is null)
            throw new NotFoundException("Operation failed.", nameof(Assembly));

        await _assemblyRepository.AddManagersToAssembly(request.AssemblyId, request.ManageUsernames);

        var response = new BaseResponse()
        {
            Success = true,
            Message = "Operation was successful"
        };

        return response;
    }
}
