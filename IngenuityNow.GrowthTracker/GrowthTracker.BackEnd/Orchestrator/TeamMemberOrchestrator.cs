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
}

public class TeamMemberOrchestrator : ITeamMemberOrchestrator
{
    private readonly IDataService<TeamMember> _teamMemberDataService;
    private readonly ITeamMemberCompetencyDataService _teamMemberCompetencyDataService;
    private readonly IUnitOfWork<IGrowthTrackerContext> _unitOfWork;

    public TeamMemberOrchestrator(IDataService<TeamMember> teamMemberDataService, IUnitOfWork<IGrowthTrackerContext> unitOfWork, ITeamMemberCompetencyDataService teamMemberCompetencyDataService)
    {
        _teamMemberDataService = teamMemberDataService;
        _unitOfWork = unitOfWork;
        _teamMemberCompetencyDataService = teamMemberCompetencyDataService;
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
        //competency.ObjectState = ObjectState.Added;
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
}
