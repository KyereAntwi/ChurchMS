using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.Districts.Commands.CreateADistrict;

public record CreateADistrictCommand(
    string Title,
    string Area,
    string DistrictPastorFullName
    ) : IRequest<BaseResponse>;

public class CreateADistrictCommandHandler : IRequestHandler<CreateADistrictCommand, BaseResponse>
{
    private readonly IAsyncRepository<District> _asyncRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateADistrictCommandHandler(IAsyncRepository<District> asyncRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse> Handle(CreateADistrictCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validation = new CreateADistrictCommandValidator();
        var validationErrors = await validation.ValidateAsync(request, cancellationToken);

        if (validationErrors.Errors.Any())
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();

            foreach ( var validationError in validationErrors.Errors )
                response.ValidationErrors.Add( validationError.ErrorMessage );

            response.Message = "Operation failed due some validation errors.";

            return response;
        }

        var district = _mapper.Map<District>( request );

        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        district.CreatedBy = userId;
        district.UpdatedAt = DateTime.UtcNow;
        district.CreatedAt = DateTime.UtcNow;

        var newDistrict = await _asyncRepository.AddAsync( district );

        response.Data = newDistrict.Id;

        response.Success = true;
        response.Message = "Operation was successful";

        return response;
    }
}
