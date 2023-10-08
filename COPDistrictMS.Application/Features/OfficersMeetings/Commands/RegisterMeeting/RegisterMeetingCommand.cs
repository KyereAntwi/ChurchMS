using System.Security.Claims;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Exceptions;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace COPDistrictMS.Application.Features.OfficersMeetings.Commands.RegisterMeeting;

public record RegisterMeetingCommand(
    Guid DistrictId,
    string PastorInCharge,
    List<Guid> MemberIds) : IRequest<BaseResponse>;

public class RegisterMeetingCommandHandler : IRequestHandler<RegisterMeetingCommand, BaseResponse>
{
    private readonly IAsyncRepository<District> _asyncRepository;
    private readonly IAsyncRepository<Member> _memberRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RegisterMeetingCommandHandler(
        IAsyncRepository<District> asyncRepository, 
        IAsyncRepository<Member> memberRepo,
        IHttpContextAccessor httpContextAccessor)
    {
        _asyncRepository = asyncRepository;
        _memberRepo = memberRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse> Handle(RegisterMeetingCommand request, CancellationToken cancellationToken)
    {
        var district = await _asyncRepository.GetByIdAsync(request.DistrictId);

        if (district is null)
            throw new NotFoundException($"Specified district with Id {request.DistrictId}!", nameof(District));

        var validation = new RegisterMeetingCommandValidator();
        var validationResult = await validation.ValidateAsync(request, cancellationToken);
        
        var response = new BaseResponse();
        response.ValidationErrors = new List<string>();

        if (validationResult.Errors.Any())
        {
            response.Success = false;
            foreach (var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }
        
        // if validation errors passes continue from here ...
        
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var newMeeting = new OfficersMeeting()
        {
            PastorInCharge = request.PastorInCharge,
            DistrictId = request.DistrictId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = userId
        };

        foreach (var memberId in request.MemberIds)
        {
            var member = await _memberRepo.GetByIdAsync(memberId);

            if (member is null)
            {
                response.ValidationErrors.Add(
                    $"Specified member with Id {memberId} was not found and failed to be register as an attendant to the meeting");
            }
            else
            {
                newMeeting.Officers.Add(member);
            }
        }

        if (newMeeting.Officers.Any())
        {
            await _asyncRepository.SaveAsync();
        }
        else
        {
            response.Success = false;

            if (response.ValidationErrors.Any())
                response.Message = "Operation completed successfully with some errors";
        }

        return response;
    }
} 