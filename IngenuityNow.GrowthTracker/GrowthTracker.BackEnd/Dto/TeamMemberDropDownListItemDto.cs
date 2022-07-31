namespace GrowthTracker.BackEnd.Dto;
public record TeamMemberDropDownListItemDto
{
    public TeamMemberDropDownListItemDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; }
}
