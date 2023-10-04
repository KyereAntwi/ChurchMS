using AutoMapper;
using COPDistrictMS.Application.Features.Districts.Commands.CreateADistrict;
using COPDistrictMS.Application.Features.Districts.Commands.CreateDistrictWithAssemblies;
using COPDistrictMS.Application.Features.Districts.Commands.UpdateDistrict;
using COPDistrictMS.Application.Features.Dtos;
using COPDistrictMS.Application.Features.Members.Commands.AddAMember;
using COPDistrictMS.Application.Features.Members.Commands.UpdateMember;
using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Commons;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<District, DistrictDto>();
        CreateMap<CreateAssemblyDto, Assembly>();
        CreateMap<CreateADistrictCommand, District>();
        CreateMap<CreateDistrictWithAssembliesCommand, District>();
        CreateMap<UpdateDistrictCommand, District>();
        CreateMap<Address,  AddressDto>();
        CreateMap<Assembly, AssemblyDto>();
        CreateMap<AddAMemberCommand, Member>();
        CreateMap<AddAMemberCommand, Address>();
        CreateMap<UpdateMemberCommand, Member>();
        CreateMap<UpdateMemberCommand, Address>();
    }
}
