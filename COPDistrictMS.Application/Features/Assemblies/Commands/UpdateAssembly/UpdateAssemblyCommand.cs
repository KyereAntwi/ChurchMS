using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;

namespace COPDistrictMS.Application.Features.Assemblies.Commands.UpdateAssembly;

public record UpdateAssemblyCommand(
    Guid Id,
    string Title,
    string Location,
    string YearEstablished
    ) : IRequest<BaseResponse>;

public class UpdateAssemblyCommandHandler : IRequestHandler<UpdateAssemblyCommand, BaseResponse>
{
    private readonly IAsyncRepository<Assembly> _asyncRepository;
    private readonly IMapper _mapper;

    public UpdateAssemblyCommandHandler(IAsyncRepository<Assembly> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(UpdateAssemblyCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validation = new UpdateAssemblyCommandValidator(_asyncRepository);
        var validationResponse = await validation.ValidateAsync(request, cancellationToken);

        if ( validationResponse.Errors.Any()) 
        {
            response.Success = false;
            response.Message = "Operation to update Assembly failed";
            response.ValidationErrors = new List<string>();

            foreach ( var error in validationResponse.Errors )
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        var assembly = await _asyncRepository.GetByIdAsync(request.Id);

        assembly!.Title = request.Title; 
        assembly.Location = request.Location;
        assembly.YearEstablished = request.YearEstablished;
        assembly.UpdatedAt = DateTime.UtcNow;

        await _asyncRepository.UpdateAsync(assembly);
        
        response.Success = true;
        response.Message = "Operation to update assembly was successful";
        response.Data = assembly.Id;

        return response;
    }
}
