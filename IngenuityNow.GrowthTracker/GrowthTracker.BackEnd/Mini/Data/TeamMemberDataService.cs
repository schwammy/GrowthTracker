using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Model;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using Microsoft.EntityFrameworkCore;


namespace GrowthTracker.BackEnd.Mini.Data;

public interface ITeamMemberDataService: IDataService<TeamMember>
{
    Task<GetTeamMemberEvaluationDto> GetTeamMemberEvaluationAsync(int teamMemberId);
}

public class TeamMemberDataService : DataService<TeamMember>, ITeamMemberDataService
{
    private readonly IGenericRepository<Role> _roleRepository;

    public TeamMemberDataService(IGenericRepository<TeamMember> repository, IGenericRepository<Role> roleRepository) : base(repository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<GetTeamMemberEvaluationDto> GetTeamMemberEvaluationAsync(int teamMemberId)
    {
        var tm = await Repository.AllIncluding(tm => tm.Competencies).SingleOrDefaultAsync(tm => tm.Id == teamMemberId);
        var role = await _roleRepository.FindAsync(tm.RoleId);
        var rcs = await _roleRepository.AllIncluding(role => role.RoleCompetencyLink).ThenInclude(rc => rc.Competency).Where(x => x.Id == tm.RoleId).SelectMany(sm => sm.RoleCompetencyLink).ToListAsync();

        var aaa = from rc in rcs
                  join tmc in tm.Competencies on rc.Competency.Id equals tmc.CompetencyId into j
                  from tmc in j.DefaultIfEmpty()
                  where tmc == null ? true : !tmc.IsArchived
                  select new GetRoleCompetencyDto
                  {
                      CompetencyId = rc.Competency.Id,
                      Title = rc.Competency.Title,
                      KeyArea = rc.Competency.KeyArea,
                      Attribute = rc.Competency.Attribute,
                      Level1Description = rc.Competency.Level1Description,
                      Level2Description = rc.Competency.Level2Description,
                      Level3Description = rc.Competency.Level3Description,
                      Level4Description = rc.Competency.Level4Description,
                      Level5Description = rc.Competency.Level5Description,
                      Level6Description = rc.Competency.Level6Description,
                      Level = tmc == null ? 0 : tmc.Level,
                      EvaluatedBy = tmc == null ? string.Empty : tmc.EvaluatedById.ToString(),
                      AchievedDate = tmc == null ? DateOnly.MinValue : DateOnly.FromDateTime(tmc.AchievedDate)
                  };

        var result = new GetTeamMemberEvaluationDto();
        result.Level1Title = role.Level1Title;
        result.Level2Title = role.Level2Title;
        result.Level3Title = role.Level3Title;
        result.Level4Title = role.Level4Title;
        result.Level5Title = role.Level5Title;
        result.Level6Title = role.Level6Title;
        result.Role = role.Name;
        result.FirstName = tm.FirstName;
        result.LastName = tm.LastName;
        result.TeamMemberId = tm.Id;

        result.RoleCompetencies = aaa.ToList();

        return result;

        //var x = from roleComp in _roleRepository.AllIncluding(role => role.RoleCompetencyLink).ThenInclude(rc => rc.Competency).Select(sm => sm)
        //        join teamMemberComp in Repository.AllIncluding(t => t.Competencies).SelectMany(t => t.Competencies) on roleComp.Id equals teamMemberComp.CompetencyId
        //foreach(var rc in x.RoleCompetencyLink)
        //{
        //    rc.Competency.
        //}
        //Repository.AllIncluding(tm => tm.Competencies);
    }
}
