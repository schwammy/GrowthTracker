namespace GrowthTracker.BackEnd.Dto;
public class GetRoleCompetencyDto
{
    public int CompetencyId { get; set; }
    public string Title { get; set; }
    public string KeyArea { get; set; }
    public string Attribute { get; set; }
    public string Level1Description { get; set; }
    public string Level2Description { get; set; }
    public string Level3Description { get; set; }
    public string Level4Description { get; set; }
    public string Level5Description { get; set; }
    public string Level6Description { get; set; }

    public int Level { get; set; }
    public string EvaluatedBy { get; set; }
    public DateOnly AchievedDate { get; set; }

    // maybe?
    public int ExpectedLevel1 { get; set; }
    public int ExpectedLevel2 { get; set; }
    public int ExpectedLevel3 { get; set; }
    public int ExpectedLevel4 { get; set; }
    public int ExpectedLevel5 { get; set; }
    public int ExpectedLevel6 { get; set; }
}
