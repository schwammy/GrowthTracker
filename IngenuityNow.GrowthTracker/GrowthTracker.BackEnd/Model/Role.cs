using IngenuityNow.Common.Data;

namespace GrowthTracker.BackEnd.Model;
public class Role : IntegerIdEntity
{
    public string Name { get; set; }
    public string RoleGroup { get; set; }
    public string Level1Title { get; set; }
    public string Level2Title { get; set; }
    public string Level3Title { get; set; }
    public string Level4Title { get; set; }
    public string Level5Title { get; set; }
    public string Level6Title { get; set; }


    public ICollection<RoleCompetency> RoleCompetencyLink { get; set; }
}
