namespace GrowthTracker.BackEnd.Dto;
public class GetTeamMemberDto
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime StartDate { get; init; }
    public string Team { get; init; }
    public string Role { get; init; }

}
