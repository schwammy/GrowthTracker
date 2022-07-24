namespace GrowthTracker.BackEnd.Dto;
public class GetTeamMemberEvaluationDto 
{
    public int TeamMemberId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public List<GetRoleCompetencyDto> RoleCompetencies { get; set; } = new List<GetRoleCompetencyDto>();

    public string Level1Title { get; set; }
    public string Level2Title { get; set; }
    public string Level3Title { get; set; }
    public string Level4Title { get; set; }
    public string Level5Title { get; set; }
    public string Level6Title { get; set; }
}
