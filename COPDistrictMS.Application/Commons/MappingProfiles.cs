using AutoMapper;
using COPDistrictMS.Application.Features.Districts.Commands.CreateADistrict;
using COPDistrictMS.Application.Features.Districts.Commands.CreateDistrictWithAssemblies;
using COPDistrictMS.Application.Features.Districts.Commands.UpdateDistrict;
using COPDistrictMS.Application.Features.Dtos;
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
    }
}
