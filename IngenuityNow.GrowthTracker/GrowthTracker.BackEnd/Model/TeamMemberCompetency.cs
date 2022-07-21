
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


    [ForeignKey("CompetencyId")]
    public Competency? Competency { get; set; }

    [ForeignKey("TeamMemberId")] 
    public TeamMember? TeamMember { get; set; }
    
    [ForeignKey("EvaluatedById")]
    public TeamMember? EvaluatedBy { get; set; }

}
