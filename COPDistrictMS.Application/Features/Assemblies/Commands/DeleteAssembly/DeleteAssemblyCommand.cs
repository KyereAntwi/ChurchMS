using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;
using COPDistrictMS.Application.Exceptions;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.DeleteAssembly;

public record DeleteAssemblyCommand(
    Guid AssemblyId
    ) : IRequest<BaseResponse>;

public class DeleteAssemblyCommandHandler : IRequestHandler<DeleteAssemblyCommand, BaseResponse>
{
    private readonly IAsyncRepository<Assembly> _asyncRepository;

    public DeleteAssemblyCommandHandler(IAsyncRepository<Assembly> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<BaseResponse> Handle(DeleteAssemblyCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var assembly = await _asyncRepository.GetByIdAsync(request.AssemblyId);

        if (assembly == null)
            throw new NotFoundException($"Specified assembly with Id {request.AssemblyId} was not found", typeof(Assembly));

        await _asyncRepository.DeleteAsync(assembly);

        response.Success = true;
        response.Message = "Operation was successful";

        return response;
    }
}
