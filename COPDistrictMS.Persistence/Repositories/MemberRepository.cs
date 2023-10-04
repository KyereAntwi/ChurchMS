using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using COPDistrictMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace COPDistrictMS.Persistence.Repositories;

public class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    private readonly COPDistrictMSContext _dbContext;

    public MemberRepository(COPDistrictMSContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Member?> GetFullDetailsAsync(Guid memberId) => await
        _dbContext
            .Members
            .Include(m => m.Address)
            .Include(m => m.OfficerType)
            .Include(m => m.Assembly)
            .FirstOrDefaultAsync(m => m.Id == memberId);

    public async Task<(IReadOnlyList<Member>, int)> GetMembersByAssembly(Guid assemblyId, int page, int size)
    {
        var list = _dbContext.Members.Include(m => m.Assembly).AsQueryable();
        
        list = list
            .Skip((page - 1) * size).Take(size)
            .AsNoTracking()
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.GivingName)
            .Select(m => new Member()
            {
                Id = m.Id,
                GivingName = m.GivingName,
                LastName = m.LastName,
                OtherNames = m.OtherNames,
                PhotographUrl = m.PhotographUrl,
                Gender = m.Gender,
                DateOfBirth = m.DateOfBirth,
                Assembly = new Assembly(){ Title = m.Assembly!.Title }
            });

        return (await list.ToListAsync(), list.Count());
    }

    public async Task<(IReadOnlyList<Member>, int)> FilterMembersByDistrict(Guid assemblyId, Guid districtId, string nameString, string gender,
        DateOnly dateOfBirth, int year, int month, int page, int size)
    {
        var list = _dbContext.Members
            .Include(m => m.Assembly)
            .Include(m => m.OfficerType)
            .AsQueryable();

        if (assemblyId != Guid.Empty)
        {
            list = list.Where(m => m.Assembly!.DistrictId == districtId);
        }

        if (assemblyId != Guid.Empty)
        {
            list = list.Where(m => m.Assembly!.Id == districtId);
        }

        if (string.IsNullOrWhiteSpace(nameString))
        {
            list = list.Where(m => m.LastName.ToLower().Contains(nameString.ToLower())
            || m.GivingName.ToLower().Contains(nameString.ToLower()));
        }

        if (string.IsNullOrWhiteSpace(gender))
        {
            list = list.Where(m => m.Gender.ToLower().Contains(gender.ToLower()));
        }

        if (dateOfBirth != new DateOnly())
        {
            list = list.Where(m => m.DateOfBirth == dateOfBirth);
        }

        if (year > 0)
        {
            list = list.Where(m => m.DateOfBirth.Year == year);
        }

        if (month > 0)
        {
            list = list.Where(m => m.DateOfBirth.Month == month);
        }
        
        list = list
            .Skip((page - 1) * size).Take(size)
            .AsNoTracking()
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.GivingName).Select(m => new Member()
            {
                Id = m.Id,
                GivingName = m.GivingName,
                LastName = m.LastName,
                OtherNames = m.OtherNames,
                PhotographUrl = m.PhotographUrl,
                Gender = m.Gender,
                DateOfBirth = m.DateOfBirth,
                Assembly = new Assembly(){ Title = m.Assembly!.Title }
            });

        return (await list.ToListAsync(), list.Count());
    }
}