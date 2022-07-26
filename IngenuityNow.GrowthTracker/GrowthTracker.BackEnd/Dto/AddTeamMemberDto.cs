﻿using System.ComponentModel.DataAnnotations;

namespace GrowthTracker.BackEnd.Dto;

public class AddTeamMemberDto
{
    public AddTeamMemberDto()
    {

    }
    public AddTeamMemberDto(string firstName, string lastName, DateTime startDate, int teamId, int roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        StartDate = startDate;
        TeamId = teamId;
        RoleId = roleId;
    }
    
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
