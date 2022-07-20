CREATE TABLE [dbo].[TeamMemberCompetencies]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CompetencyId] INT NOT NULL, 
    [Level] INT NOT NULL, 
    [AchievedDate] DATETIME NOT NULL
)
