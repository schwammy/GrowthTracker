CREATE TABLE [dbo].[RoleCompetencies]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoleId] INT NOT NULL, 
	[CompetencyId] INT NOT NULL, 
    [EffectiveDate] DATETIME NOT NULL, 
	[EndDate] DATETIME NULL, 
	[ExpectedLevel1] INT NOT NULL, 
	[ExpectedLevel2] INT NOT NULL, 
	[ExpectedLevel3] INT NOT NULL, 
	[ExpectedLevel4] INT NOT NULL, 
	[ExpectedLevel5] INT NOT NULL, 
	[ExpectedLevel6] INT NOT NULL, 
	    CONSTRAINT [FK_RoleCompetencies_RoleId] FOREIGN KEY (RoleId) REFERENCES [Roles]([Id]),
	    CONSTRAINT [FK_RoleCompetencies_CompetencyId] FOREIGN KEY (CompetencyId) REFERENCES [Competencies]([Id]),

    
)
