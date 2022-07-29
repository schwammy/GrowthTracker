using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Mini.Data;
using GrowthTracker.BackEnd.Model;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using IngenuityNow.Common.Result;
using Microsoft.EntityFrameworkCore;

namespace GrowthTracker.BackEnd.Orchestrator;

public interface ITeamMemberOrchestrator
{
    Task<ItemResult<AddTeamMemberDto>> AddTeamMemberAsync(AddTeamMemberDto dto);
    Task<Result> SetTeamMemberCompetencyLevel(SetTeamMemberCompetencyLevelDto dto);
    Task<ListResult<TeamMemberCompetency>> GetCompetenciesForTeamMember(int id);
    Task<ListResult<TeamMemberListItemDto>> GetTeamMemberListItems();
    Task<ItemResult<GetTeamMemberEvaluationDto>> GetTeamMemberEvaluation(int teamMemberId);
    Task<Result> SetTeamMemberCompetencyLevel(List<SetTeamMemberCompetencyLevelDto> dtos);
    Task Test();
}

public class TeamMemberOrchestrator : ITeamMemberOrchestrator
{
    private readonly IDataService<TeamMember> _teamMemberDataService;
    private readonly ITeamMemberCompetencyDataService _teamMemberCompetencyDataService;
    private readonly IDataService<RoleCompetency> _roleCompetencyDataService;
    private readonly IDataService<Competency> _competencyDataService;
    private readonly IDataService<Role> _roleDataService;
    private readonly IUnitOfWork<IGrowthTrackerContext> _unitOfWork;
    //private readonly IGrowthTrackerContext _growthTrackerContext;

    public TeamMemberOrchestrator(IDataService<TeamMember> teamMemberDataService, IUnitOfWork<IGrowthTrackerContext> unitOfWork, ITeamMemberCompetencyDataService teamMemberCompetencyDataService, IDataService<RoleCompetency> roleCompetencyDataService, IDataService<Competency> competencyDataService, IDataService<Role> roleDataService)
    {
        _teamMemberDataService = teamMemberDataService;
        
        _unitOfWork = unitOfWork;
        _teamMemberCompetencyDataService = teamMemberCompetencyDataService;
        _roleCompetencyDataService = roleCompetencyDataService;
        _competencyDataService = competencyDataService;
        _roleDataService = roleDataService;
        //_growthTrackerContext = growthTrackerContext;
    }

    public async Task<ItemResult<AddTeamMemberDto>> AddTeamMemberAsync(AddTeamMemberDto dto)
    {
        var teamMember = new TeamMember { FirstName = dto.FirstName, LastName = dto.LastName, RoleId = dto.RoleId, TeamId = dto.TeamId, StartDate = dto.StartDate };
        _teamMemberDataService.Add(teamMember);
        await _unitOfWork.SaveAllAsync();

        return ItemResult<AddTeamMemberDto>.Success(dto);

    }

    public async Task<Result> SetTeamMemberCompetencyLevel(SetTeamMemberCompetencyLevelDto dto)
    {
        var teamMember = await _teamMemberDataService.GetAsync(dto.TeamMemberId);
        var competency = new TeamMemberCompetency();
        competency.EvaluatedById = dto.EvaluatedById;
        competency.CompetencyId = dto.CompetencyId;
        competency.AchievedDate = dto.AchievedDate;
        competency.TeamMemberId = dto.TeamMemberId;
        competency.Level = dto.Level;
        teamMember.Competencies.Add(competency);

        await _unitOfWork.SaveAllAsync();

        return Result.Success();
    }

    public async Task<Result> SetTeamMemberCompetencyLevel(List<SetTeamMemberCompetencyLevelDto> dtos)
    {
        var teamMemberId = dtos.FirstOrDefault().TeamMemberId;

        var teamMember = await _teamMemberDataService.ListIncluding(tm => tm.Competencies).SingleAsync(tm => tm.Id == teamMemberId);


        foreach (var dto in dtos)
        {
            // first mark old 
            teamMember.Competencies.Where(c => c.CompetencyId == dto.CompetencyId).ToList().ForEach(c => c.IsArchived = true);  

            // now add new
            var competency = new TeamMemberCompetency();
            competency.EvaluatedById = dto.EvaluatedById;
            competency.CompetencyId = dto.CompetencyId;
            competency.AchievedDate = dto.AchievedDate;
            competency.TeamMemberId = dto.TeamMemberId;
            competency.Level = dto.Level;
            teamMember.Competencies.Add(competency);
        }
        await _unitOfWork.SaveAllAsync();

        return Result.Success();
    }

    public async Task<ItemResult<TeamMember>> GetTeamMember(int id)
    {
        var teamMember = await _teamMemberDataService.GetAsync(id);

        return ItemResult<TeamMember>.Success(teamMember);
    }

    public async Task<ListResult<TeamMemberCompetency>> GetCompetenciesForTeamMember(int id)
    {
        var results = await _teamMemberCompetencyDataService.GetTeamMemberCompetenciesAsync(id);

        return ListResult<TeamMemberCompetency>.Success(results);
    }

    public async Task<ListResult<TeamMemberListItemDto>> GetTeamMemberListItems()
    {
        var all = await _teamMemberDataService.ListAsync();
        var result = all.Select(a => new TeamMemberListItemDto(a.Id, $"{a.FirstName} {a.LastName}"));
        return ListResult<TeamMemberListItemDto>.Success(result);
    }

    public async Task<ItemResult<GetTeamMemberEvaluationDto>> GetTeamMemberEvaluation(int teamMemberId)
    {
        GetTeamMemberEvaluationDto result = new GetTeamMemberEvaluationDto();
        result.TeamMemberId = teamMemberId;

        var teamMember = await _teamMemberDataService.GetAsync(teamMemberId);
        var role = await _roleDataService.GetAsync(teamMember.RoleId);

        result.Level1Title = role.Level1Title;
        result.Level2Title = role.Level2Title;
        result.Level3Title = role.Level3Title;
        result.Level4Title = role.Level4Title;
        result.Level5Title = role.Level5Title;
        result.Level6Title = role.Level6Title;



        var tmc = await _teamMemberCompetencyDataService.GetTeamMemberCompetenciesAsync(teamMemberId);

        // todo: need to filter by role
        var rcs = await _roleCompetencyDataService.ListIncluding(rc => rc.Competency).ToListAsync();

        var roleAndComps = from roleCompetency in rcs
                           join memberLevelTable in tmc
                           on roleCompetency.CompetencyId equals memberLevelTable.CompetencyId into j
                           from memberLevel in j.DefaultIfEmpty()
                           where roleCompetency.RoleId == 2 
                           && (memberLevel == null ? true : memberLevel.IsArchived == false)
                           select new { RoleCompetency = roleCompetency, Level = memberLevel };

        foreach (var item in roleAndComps)
        {
            GetRoleCompetencyDto dto = new GetRoleCompetencyDto();
            dto.KeyArea = item.RoleCompetency.Competency.KeyArea;
            dto.Attribute = item.RoleCompetency.Competency.Attribute;
            dto.Title = item.RoleCompetency.Competency.Title;
            dto.CompetencyId = item.RoleCompetency.Competency.Id;
            dto.Level1Description = item.RoleCompetency.Competency.Level1Description;
            dto.Level2Description = item.RoleCompetency.Competency.Level2Description;
            dto.Level3Description = item.RoleCompetency.Competency.Level3Description;
            dto.Level4Description = item.RoleCompetency.Competency.Level4Description;
            dto.Level5Description = item.RoleCompetency.Competency.Level5Description;
            dto.Level6Description = item.RoleCompetency.Competency.Level6Description;
            
            

            if(item.Level != null)
            {
                dto.Level = item.Level.Level;
                dto.AchievedDate = DateOnly.FromDateTime(item.Level.AchievedDate);
                dto.EvaluatedBy = item.Level.EvaluatedById.ToString();
            }

            result.RoleCompetencies.Add(dto);
        }

        return ItemResult<GetTeamMemberEvaluationDto>.Success(result);
    }

    public async Task Test()
    {
        //Parent p = new Parent { Name = "Dad" };
        //p.Children.Add(new Child { Name = "Benny" });
        //p.Children.Add(new Child { Name = "Sarah" });

        //_growthTrackerContext.Parents.Add(p);
        //await _growthTrackerContext.SaveChangesAsync();

        //var teamMember = await _growthTrackerContext.TeamMembers.FindAsync(1);

        //TeamMemberCompetency comp1 = new TeamMemberCompetency();
        //comp1.EvaluatedById = 1;
        //comp1.CompetencyId = 12;
        //comp1.AchievedDate = DateTime.Now;
        //comp1.Level = 21;

        //TeamMemberCompetency comp2 = new TeamMemberCompetency();
        //comp2.EvaluatedById = 1;
        //comp2.CompetencyId = 13;
        //comp2.AchievedDate = DateTime.Now;
        //comp2.Level = 22;

        //teamMember.Competencies.Add(comp1);
        //teamMember.Competencies.Add(comp2);

        //await _growthTrackerContext.SaveChangesAsync();
    }
}
