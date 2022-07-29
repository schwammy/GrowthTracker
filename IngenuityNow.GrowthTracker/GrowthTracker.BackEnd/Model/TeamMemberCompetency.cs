
using IngenuityNow.Common.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowthTracker.BackEnd.Model;

public class TeamMemberCompetency : IntegerIdEntity
{
    public int CompetencyId { get; set; }
    public int Level { get; set; }
    public DateTime AchievedDate { get; set; }
    public int TeamMemberId { get; set; }
    public int EvaluatedById { get; set; }
    public bool IsArchived { get; set; }

    public Competency? Competency { get; set; }

    public TeamMember? TeamMember { get; set; }
    

}
