using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Application.Contracts.Infrastructure;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SVoting.Application.Exceptions;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.Members.Commands.AddAMember;

public record AddAMemberCommand(
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

public class AddAMemberCommandHandler : IRequestHandler<AddAMemberCommand, BaseResponse>
{
    private readonly IAsyncRepository<Member> _asyncRepository;
    private readonly IAssemblyRepository _assemblyRepository;
    private readonly IOfficerRepository _officerRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddAMemberCommandHandler(
        IAsyncRepository<Member> asyncRepository, 
        IMapper mapper,
        IAssemblyRepository assemblyRepository,
        IOfficerRepository officerRepository,
        IImageService imageService,
        IHttpContextAccessor httpContextAccessor)
    {
        _asyncRepository = asyncRepository;
        _assemblyRepository = assemblyRepository;
        _officerRepository = officerRepository;
        _imageService = imageService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse> Handle(AddAMemberCommand request, CancellationToken cancellationToken)
    {
        Assembly? memberAssembly = null;
        OfficerType? memberOffice = null;

        if ( request.AssemblyId == Guid.Empty ) 
        {
            var assembly = await _assemblyRepository.GetByIdAsync( request.AssemblyId );
            if ( assembly == null )
            {
                throw new NotFoundException($"The specified assembly with Id {request.AssemblyId} was not found", typeof(Assembly));
            }

            memberAssembly = assembly;
        }

        if (string.IsNullOrWhiteSpace(request.OfficeType))
        {
            var officerType = await _officerRepository.GetByType(request.OfficeType);
            if ( officerType == null )
            {
                throw new NotFoundException($"The specified officer type with name {request.OfficeType} is not registered", typeof(OfficerType));
            }

            memberOffice = officerType;
        }

        var response = new BaseResponse();

        var member = _mapper.Map<Member>( request );
        var address = _mapper.Map<Address>( request );

        member.Address = address;
        member.CreatedAt = DateTime.UtcNow;
        member.UpdatedAt = DateTime.UtcNow;

        // Accessing the logged-in user's ID
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        member.CreatedBy = userId;

        member.CreatedBy = request.LoggedInUsername;

        if(memberAssembly is not null)
            member.Assembly = memberAssembly;

        if(memberOffice is not null)
            member.OfficerType = memberOffice;

        // work on member photograph
        if(request.Photograph is not null)
        {
            member.PhotographUrl = await _imageService.UploadFileToFirebase( request.Photograph );
        }

        var newMember = await _asyncRepository.AddAsync(member );

        response.Success = true;
        response.Message = "Operation to add a new member was successful";
        response.Data = newMember.Id;

        return response;
    }
}
