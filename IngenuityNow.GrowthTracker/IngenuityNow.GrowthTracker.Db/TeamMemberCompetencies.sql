CREATE TABLE [dbo].[TeamMemberCompetencies]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TeamMemberId] INT NOT NULL,
    [CompetencyId] INT NOT NULL, 
    [Level] INT NOT NULL, 
    [AchievedDate] DATETIME NOT NULL, 
    [EvaluatedById] INT NOT NULL,
    CONSTRAINT [FK_TeamMemberCompetencies_CompetencyId] FOREIGN KEY (CompetencyId) REFERENCES [Competencies]([Id]),
    CONSTRAINT [FK_TeamMemberCompetencies_TeamMemberId] FOREIGN KEY (TeamMemberId) REFERENCES [TeamMembers]([Id]),
    CONSTRAINT [FK_TeamMemberCompetencies_EvaluatedById] FOREIGN KEY (EvaluatedById) REFERENCES [TeamMembers]([Id]),
)
