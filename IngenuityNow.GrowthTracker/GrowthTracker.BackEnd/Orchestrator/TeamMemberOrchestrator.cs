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
}

public class TeamMemberOrchestrator : ITeamMemberOrchestrator
{
    private readonly ITeamMemberDataService _teamMemberDataService;
    private readonly ITeamMemberCompetencyDataService _teamMemberCompetencyDataService;
    private readonly IUnitOfWork<IGrowthTrackerContext> _unitOfWork;

    public TeamMemberOrchestrator(ITeamMemberDataService teamMemberDataService, IUnitOfWork<IGrowthTrackerContext> unitOfWork, ITeamMemberCompetencyDataService teamMemberCompetencyDataService)
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
        var eval = await _teamMemberDataService.GetTeamMemberEvaluationAsync(teamMemberId);


        return ItemResult<GetTeamMemberEvaluationDto>.Success(eval);
    }
}
