namespace GrowthTracker.BackEnd.Dto;
public record GetTeamMemberLevelDto (int CompetencyId, int Level, string EvaluatedBy, DateOnly AchievedDate)
{

}
