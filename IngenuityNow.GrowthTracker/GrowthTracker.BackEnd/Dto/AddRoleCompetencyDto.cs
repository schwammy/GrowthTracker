namespace GrowthTracker.BackEnd.Dto;
public class AddRoleCompetencyDto
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

}
