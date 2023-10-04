using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.OfficeTypes.Commands.CreateOfficeType;

public record CreateOfficeTypeCommand(
   string Office
    ) : IRequest<BaseResponse>;

public class CreateOfficeTypeCommandHandler : IRequestHandler<CreateOfficeTypeCommand, BaseResponse>
{
    private readonly IOfficerRepository _asyncRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CreateOfficeTypeCommandHandler(IOfficerRepository asyncRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _asyncRepository = asyncRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(CreateOfficeTypeCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validation = new CreateOfficeTypeCommandValidator(_asyncRepository);
        var validationResult = await validation.ValidateAsync(request, cancellationToken);

        if(validationResult.Errors.Any()) 
        {
            response.ValidationErrors = new List<string>();
            response.Success = false;
            foreach( var error in validationResult.Errors )
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
            return response;
        }

        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var officeType = _mapper.Map<OfficerType>(request);
        officeType.CreatedBy = userId;
        officeType.CreatedAt = DateTime.UtcNow;
        officeType.UpdatedAt = DateTime.UtcNow;

        var newOffice = await _asyncRepository.AddAsync(officeType);

        response.Data = newOffice.Id;
        response.Success = true;
        response.Message = "Operation to create Office was successful";

        return response;
    }
}
