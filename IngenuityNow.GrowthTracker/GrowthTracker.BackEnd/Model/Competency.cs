using IngenuityNow.Common.Data;

namespace GrowthTracker.BackEnd.Model;

public class Competency : IntegerIdEntity
{
    public string Title { get; set; }
    public string KeyArea { get; set; }
    public string Attribute { get; set; }
    public string Level1Description { get; set; }
    public string Level2Description { get; set; }
    public string Level3Description { get; set; }
    public string Level4Description { get; set; }
    public string Level5Description { get; set; }
    public string Level6Description { get; set; }

    public ICollection<RoleCompetency> RoleCompetencyLink { get; set; }
}
