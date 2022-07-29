using IngenuityNow.Common.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowthTracker.BackEnd.Model;

public class TeamMember : IntegerIdEntity
{
    public TeamMember()
    {
        Competencies = new List<TeamMemberCompetency>();
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime StartDate { get; set; }
    public int TeamId { get; set; }
    public int RoleId { get; set; }

    public List<TeamMemberCompetency> Competencies { get; private set; }
}
