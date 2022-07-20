
using IngenuityNow.Common.Data;

namespace GrowthTracker.BackEnd.Model;

public class TeamMemberCompetency : IntegerIdEntity
{
    public int CompetencyId { get; set; }
    public int Level { get; set; }
    public DateTime AchievedDate { get; set; }

}
