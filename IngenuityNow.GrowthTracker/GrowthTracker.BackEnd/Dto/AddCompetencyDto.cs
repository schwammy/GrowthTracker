namespace GrowthTracker.BackEnd.Dto
{
    public record AddCompetencyDto
    {

        public AddCompetencyDto(string title, string keyArea, string attribute)
        {
            Title = title;
            KeyArea = keyArea;
            Attribute = attribute;
        }

        public string Title { get; init; }
        public string KeyArea { get; init; }
        public string Attribute { get; init; }
        public string Level1Description { get; set; }
        public string Level2Description { get; set; }
        public string Level3Description { get; set; }
        public string Level4Description { get; set; }
        public string Level5Description { get; set; }
        public string Level6Description { get; set; }
    }
}
