using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Infrastructure;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SVoting.Application.Exceptions;

namespace COPDistrictMS.Application.Features.Members.Commands.UpdateMember;

public record UpdateMemberCommand(
    Guid Id,
    string LastName,
    string GivingName,
    string OtherNames,
    DateOnly DateOfBirth,
    IFormFile Photograph,
    string Gender,
    Guid AssemblyId,
    string Residence,
    string PostGps,
    string NearLandmark,
    string Telephone,
    string OfficeType
    ) : IRequest<BaseResponse>;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, BaseResponse>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IOfficerRepository _officerRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public UpdateMemberCommandHandler(
        IMemberRepository memberRepository, 
        IAssemblyRepository assemblyRepository, 
        IOfficerRepository officerRepository,
        IMapper mapper,
        IImageService imageService
        )
    {
        _memberRepository = memberRepository;
        _assemblyRepository = assemblyRepository;
        _officerRepository = officerRepository;
        _imageService = imageService;
        _mapper = mapper;
    }
    public async Task<BaseResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync( request.Id );
        if (member is null)
            throw new NotFoundException($"Specified member with Id {request.Id} was not found.", typeof(Member));

        var response = new BaseResponse();

        var validation = new UpdateMemberCommandValidator();
        var validationResponse = await validation.ValidateAsync( request, cancellationToken );

        if (validationResponse.Errors.Any())
        {
            response.Success = false;
            response.Message = "Operation to update member failed due to some validation errors";
            response.ValidationErrors = new List<string>();

            foreach ( var error in validationResponse.Errors )
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        Assembly? memberAssembly = null;
        OfficerType? memberOffice = null;

        if (request.AssemblyId != Guid.Empty)
        {
            var assembly = await _assemblyRepository.GetByIdAsync(request.AssemblyId);
            if (assembly == null)
            {
                throw new NotFoundException($"The specified assembly with Id {request.AssemblyId} was not found", typeof(Assembly));
            }

            memberAssembly = assembly;
        }

        if (!string.IsNullOrWhiteSpace(request.OfficeType))
        {
            var officerType = await _officerRepository.GetByType(request.OfficeType);
            if (officerType == null)
            {
                throw new NotFoundException($"The specified officer type with name {request.OfficeType} is not registered", typeof(OfficerType));
            }

            memberOffice = officerType;
        }

        // start updating properties of the member
        var updatedMember = _mapper.Map<Member>( request );
        var updatedAddress = _mapper.Map<Address>( request );

        updatedMember.UpdatedAt = DateTime.UtcNow;
        updatedMember.Address = updatedAddress;

        if(memberAssembly != null)
            updatedMember.Assembly = memberAssembly;

        if(memberOffice != null)
            updatedMember.OfficerType = memberOffice;

        if(request.Photograph != null)
        {
            updatedMember.PhotographUrl = await _imageService.UploadFileToFirebase(request.Photograph);
        }

        await _memberRepository.UpdateAsync(updatedMember);

        response.Success = true;
        response.Data = updatedMember.Id;
        return response;
    }
}
