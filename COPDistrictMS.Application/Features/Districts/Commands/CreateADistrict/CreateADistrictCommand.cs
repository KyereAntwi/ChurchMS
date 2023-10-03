using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Districts.Commands.CreateADistrict;

public record CreateADistrictCommand(
    string Title,
    string Area,
    string DistrictPastor,
    string LoggedInUserEmail
    ) : IRequest<BaseResponse>;

public class CreateADistrictCommandHandler : IRequestHandler<CreateADistrictCommand, BaseResponse>
{
    private readonly IAsyncRepository<District> _asyncRepository;
    private readonly IMapper _mapper;

    public CreateADistrictCommandHandler(IAsyncRepository<District> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
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

        district.CreatedBy = request.LoggedInUserEmail;
        district.UpdatedAt = DateTime.UtcNow;
        district.CreatedAt = DateTime.UtcNow;

        var newDistrict = await _asyncRepository.AddAsync( district );

        response.Data = newDistrict.Id;

        response.Success = true;
        response.Message = "Operation was successful";

        return response;
    }
}
