namespace IngenuityNow.GrowthTracker.UI.Data;

public class CompetencyTableRow
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

    private int _level;
    public int OrigLevel { get; private set; }

    public bool IsDirty { get; set; }
    //public int Level { get; set; }
    public int Level {
        get {
            return _level;
        }
        set {
            if (!IsDirty)
            {
                OrigLevel = _level;
            }
            IsDirty = true;

            _level = value;
        }
    }
    public string EvaluatedBy { get; set; }
    public DateOnly AchievedDate { get; set; }

}
