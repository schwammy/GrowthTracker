CREATE TABLE [dbo].[TeamMemberCompetencies]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TeamMemberId] INT NOT NULL,
    [CompetencyId] INT NOT NULL, 
    [Level] INT NOT NULL, 
    [AchievedDate] DATETIME NOT NULL, 
    [EvaluatedById] INT NOT NULL
)
