using System.Security.Claims;
using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace COPDistrictMS.Application.Features.Offerings.Commands.AddMinistryOffering;

public record AddMinistryOfferingCommand(
    Guid DistrictId,
    string Ministry,
    decimal Amount
    ) : IRequest<BaseResponse>;

public class AddMinistryOfferingCommandHandler : IRequestHandler<AddMinistryOfferingCommand, BaseResponse>
{
    private readonly IDistrictRepository _districtRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddMinistryOfferingCommandHandler(IDistrictRepository districtRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _districtRepository = districtRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse> Handle(AddMinistryOfferingCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        
        var validation = new AddMinistryOfferingCommandValidator();
        var validationResult = await validation.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            response.Success = false;
            response.Message = "Operation to add offering failed due to validation errors";

            response.ValidationErrors = new List<string>();

            foreach (var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        var existingDistrict = await _districtRepository.GetByIdAsync(request.DistrictId);
        
        if (existingDistrict is null)
            throw new NotFoundException($"Specified district with Id {request.DistrictId}.", nameof(District));
        
        var newOffering = _mapper.Map<MinistryOffering>(request);
        
        // Accessing the logged-in user's ID
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        
        newOffering.CreatedAt = DateTime.UtcNow;
        newOffering.UpdatedAt = DateTime.UtcNow;
        newOffering.CreatedBy = userId;
        
        existingDistrict.MinistryOfferings.Add(newOffering);
        await _districtRepository.SaveAsync();

        response.Success = true;
        response.Message = "Addition of offering was successful";
        response.Data = existingDistrict.Id;
        
        return response;
    }
}