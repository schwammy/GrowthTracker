using GrowthTracker.BackEnd.Model;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using Microsoft.EntityFrameworkCore;

namespace GrowthTracker.BackEnd.Mini.Data;

public interface ITeamMemberCompetencyDataService : IDataService<TeamMemberCompetency>
{
    Task<List<TeamMemberCompetency>> GetTeamMemberCompetenciesAsync(int teamMemberId);
}

public class TeamMemberCompetencyDataService : DataService<TeamMemberCompetency>, ITeamMemberCompetencyDataService
{
    public TeamMemberCompetencyDataService(IGenericRepository<TeamMemberCompetency> repository) : base(repository)
    {
    }

    public async Task<List<TeamMemberCompetency>> GetTeamMemberCompetenciesAsync(int teamMemberId)
    {
        return await Repository.AllIncluding("Competency", "TeamMember", "EvaluatedBy").Where(x => x.TeamMemberId == teamMemberId).ToListAsync();
    }
}
