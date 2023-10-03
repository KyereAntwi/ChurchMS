using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;
using SVoting.Application.Exceptions;

namespace COPDistrictMS.Application.Features.Districts.Commands.DeleteDistrict;

public record DeleteDistrictCommand(
    Guid Id
    ) : IRequest<BaseResponse>;

public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand, BaseResponse>
{
    private readonly IAsyncRepository<District> _asyncRepository;

    public DeleteDistrictCommandHandler(IAsyncRepository<District> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<BaseResponse> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var district = await _asyncRepository.GetByIdAsync( request.Id );

        if(district == null)
            throw new NotFoundException($"Specified district with Id {request.Id} was not found", typeof(District));

        await _asyncRepository.DeleteAsync( district );

        response.Success = true;
        response.Message = "Operation was successful";

        return response;
    }
}
