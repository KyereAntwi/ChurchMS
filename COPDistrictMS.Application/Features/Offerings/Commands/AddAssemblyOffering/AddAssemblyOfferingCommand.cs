using System.Security.Claims;
using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace COPDistrictMS.Application.Features.Offerings.Commands.AddAssemblyOffering;

public record AddAssemblyOfferingCommand(
    Guid AssemblyId,
    string OfferingType,
    decimal Amount
    ) : IRequest<BaseResponse>;

public class AddAssemblyOfferingCommandHandler : IRequestHandler<AddAssemblyOfferingCommand, BaseResponse>
{
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddAssemblyOfferingCommandHandler(IAssemblyRepository assemblyRepository, IMapper mapper, 
        IHttpContextAccessor httpContextAccessor)
    {
        _assemblyRepository = assemblyRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<BaseResponse> Handle(AddAssemblyOfferingCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        
        var validation = new AddAssemblyOfferingCommandValidator();
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
        
        var existingAssembly = await _assemblyRepository.GetByIdAsync(request.AssemblyId);

        if (existingAssembly is null)
            throw new NotFoundException($"Specified assembly with Id {request.AssemblyId}.", nameof(Assembly));

        var newOffering = _mapper.Map<AssemblyOffering>(request);
        
        // Accessing the logged-in user's ID
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        
        newOffering.CreatedAt = DateTime.UtcNow;
        newOffering.UpdatedAt = DateTime.UtcNow;
        newOffering.CreatedBy = userId;

        
        existingAssembly.Offerings.Add(newOffering);
        await _assemblyRepository.SaveAsync();

        response.Success = true;
        response.Message = "Addition of offering was successful";
        response.Data = existingAssembly.Id;
        return response;
    }
}