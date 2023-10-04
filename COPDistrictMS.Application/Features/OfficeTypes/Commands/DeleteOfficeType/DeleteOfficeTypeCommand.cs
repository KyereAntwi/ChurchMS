using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using MediatR;
using SVoting.Application.Exceptions;

namespace COPDistrictMS.Application.Features.OfficeTypes.Commands.DeleteOfficeType;

public record DeleteOfficeTypeCommand(
    Guid OfficeTypeId
    ) : IRequest<BaseResponse>;

public class DeleteOfficeTypeCommandHandler : IRequestHandler<DeleteOfficeTypeCommand, BaseResponse>
{
    private readonly IOfficerRepository _officeRepository;

    public DeleteOfficeTypeCommandHandler(IOfficerRepository officerRepository)
    {
        _officeRepository = officerRepository;
    }

    public async Task<BaseResponse> Handle(DeleteOfficeTypeCommand request, CancellationToken cancellationToken)
    {
        var office = await _officeRepository.GetOfficeWithMembersAsync(request.OfficeTypeId);

        if (office is null)
            throw new NotFoundException($"Office type with Id {request.OfficeTypeId} specified does not exist", typeof(OfficerType));

        if (office.Members.Any())
            throw new BadRequestException("Specified office type has attached members. This can't be deleted");

        await _officeRepository.DeleteAsync(office);

        var response  = new BaseResponse()
        {
            Success = true,
            Data = office.Id
        };

        return response;
    }
}