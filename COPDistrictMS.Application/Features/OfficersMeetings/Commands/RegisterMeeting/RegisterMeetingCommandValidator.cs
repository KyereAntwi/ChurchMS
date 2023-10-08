using FluentValidation;

namespace COPDistrictMS.Application.Features.OfficersMeetings.Commands.RegisterMeeting;

public class RegisterMeetingCommandValidator : AbstractValidator<RegisterMeetingCommand>
{
    public RegisterMeetingCommandValidator()
    {
        RuleFor(m => m.PastorInCharge)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .NotNull();
    }
}