using GrowthTracker.BackEnd.Dto;
using GrowthTracker.BackEnd.Mini.Data;
using GrowthTracker.BackEnd.Model;
using IngenuityNow.Common.Data;
using IngenuityNow.Common.Data.DataService;
using IngenuityNow.Common.Result;

namespace GrowthTracker.BackEnd.Orchestrator;

public interface ITeamMemberOrchestrator
{
    Task<ItemResult<AddTeamMemberDto>> AddTeamMemberAsync(AddTeamMemberDto dto);
    Task<Result> SetTeamMemberCompetencyLevel(SetTeamMemberCompetencyLevelDto dto);
    Task<ListResult<TeamMemberCompetency>> GetCompetenciesForTeamMember(int id);
    Task<ListResult<TeamMemberListItemDto>> GetTeamMemberListItems();
    Task<ItemResult<GetTeamMemberEvaluationDto>> GetTeamMemberEvaluation(int teamMemberId);
}

public class TeamMemberOrchestrator : ITeamMemberOrchestrator
{
    private readonly IDataService<TeamMember> _teamMemberDataService;
    private readonly ITeamMemberCompetencyDataService _teamMemberCompetencyDataService;
    private readonly IDataService<RoleCompetency> _roleCompetencyDataService;
    private readonly IDataService<Competency> _competencyDataService;
    private readonly IDataService<Role> _roleDataService;
    private readonly IUnitOfWork<IGrowthTrackerContext> _unitOfWork;

    public TeamMemberOrchestrator(IDataService<TeamMember> teamMemberDataService, IUnitOfWork<IGrowthTrackerContext> unitOfWork, ITeamMemberCompetencyDataService teamMemberCompetencyDataService, IDataService<RoleCompetency> roleCompetencyDataService, IDataService<Competency> competencyDataService, IDataService<Role> roleDataService)
    {
        _teamMemberDataService = teamMemberDataService;
        
        _unitOfWork = unitOfWork;
        _teamMemberCompetencyDataService = teamMemberCompetencyDataService;
        _roleCompetencyDataService = roleCompetencyDataService;
        _competencyDataService = competencyDataService;
        _roleDataService = roleDataService;
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
        //competency.ObjectState = ObjectState.New;
        //teamMember.Competencies.Add(competency);

        _teamMemberCompetencyDataService.Add(competency);
        //_teamMemberDataService.Add(teamMember);
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
        var rcs = await _roleCompetencyDataService.ListAsync();
        var cs = await _competencyDataService.ListAsync();

        var roleAndComps = from roleCompetency in rcs
                           join competency in cs
                           on roleCompetency.CompetencyId equals competency.Id
                           join memberLevelTable in tmc
                           on roleCompetency.CompetencyId equals memberLevelTable.CompetencyId into j
                           from memberLevel in j.DefaultIfEmpty()
                           where roleCompetency.RoleId == 2
                           //select new { RoleCompetency = roleCompetency, Competency = competency};
                           select new { RoleCompetency = roleCompetency, Competency = competency, Level = memberLevel };

        foreach (var item in roleAndComps)
        {
            GetRoleCompetencyDto dto = new GetRoleCompetencyDto();
            dto.KeyArea = item.Competency.KeyArea;
            dto.Attribute = item.Competency.Attribute;
            dto.Title = item.Competency.Title;
            dto.CompetencyId = item.Competency.Id;
            dto.Level1Description = item.Competency.Level1Description;
            dto.Level2Description = item.Competency.Level2Description;
            dto.Level3Description = item.Competency.Level3Description;
            dto.Level4Description = item.Competency.Level4Description;
            dto.Level5Description = item.Competency.Level5Description;
            dto.Level6Description = item.Competency.Level6Description;
            
            

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
}
