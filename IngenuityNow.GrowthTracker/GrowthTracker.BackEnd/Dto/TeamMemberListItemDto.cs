﻿namespace GrowthTracker.BackEnd.Dto;
public record TeamMemberListItemDto
{
    public TeamMemberListItemDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; }
}
