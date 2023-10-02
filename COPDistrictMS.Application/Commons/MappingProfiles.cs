using AutoMapper;
using COPDistrictMS.Application.Features.Districts.Queries.GetAll;
using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Commons;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<District, DistrictDto>();
    }
}
