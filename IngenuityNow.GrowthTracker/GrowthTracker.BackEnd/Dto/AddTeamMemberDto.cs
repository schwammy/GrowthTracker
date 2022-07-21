namespace GrowthTracker.BackEnd.Dto;

public class AddTeamMemberDto
{
    public AddTeamMemberDto(string firstName, string lastName, DateTime startDate, int teamId, int roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        StartDate = startDate;
        TeamId = teamId;
        RoleId = roleId;
    }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime StartDate { get; init; }
    public int TeamId { get; init; }
    public int RoleId { get; init; }

}
