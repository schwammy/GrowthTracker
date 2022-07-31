using System.ComponentModel.DataAnnotations;

namespace GrowthTracker.BackEnd.Dto;

public class EditTeamMemberDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public int? TeamId { get; set; }
    [Required]
    public int? RoleId { get; set; }

}
