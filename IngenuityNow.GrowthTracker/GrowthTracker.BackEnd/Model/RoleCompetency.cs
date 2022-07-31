using IngenuityNow.Common.Data;

namespace GrowthTracker.BackEnd.Model;
public class RoleCompetency : IntegerIdEntity
{
    public int RoleId { get; set; }
    public int CompetencyId { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int ExpectedLevel1 { get; set; }
    public int ExpectedLevel2 { get; set; }
    public int ExpectedLevel3 { get; set; }
    public int ExpectedLevel4 { get; set; }
    public int ExpectedLevel5 { get; set; }
    public int ExpectedLevel6 { get; set; }

    public Competency? Competency { get; set; }
    public Role? Role { get; set; }
}
