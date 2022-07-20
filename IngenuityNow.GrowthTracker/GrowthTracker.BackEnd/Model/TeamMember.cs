using IngenuityNow.Common.Data;

namespace GrowthTracker.BackEnd.Model;

public class TeamMember : IntegerIdEntity
{
    public TeamMember()
    {
        CompetencyList = new List<TeamMemberCompetency>();
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime StartDate { get; set; }
    public int TeamId { get; set; }
    public int RoleId { get; set; }


    public List<TeamMemberCompetency> CompetencyList { get; private set; }
}
