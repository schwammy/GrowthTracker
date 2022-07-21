namespace GrowthTracker.BackEnd.Dto;

public class SetTeamMemberCompetencyLevelDto
{
    public int TeamMemberId { get; set; }
    public int CompetencyId { get; set; }
    public int EvaluatedById { get; set; }
    public int Level { get; set; }
    public DateTime AchievedDate { get; set; }
}
