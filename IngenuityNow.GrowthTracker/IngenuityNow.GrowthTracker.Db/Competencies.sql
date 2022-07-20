CREATE TABLE [dbo].[Competencies]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(255) NOT NULL, 
    [KeyArea] NVARCHAR(255) NOT NULL, 
    [Attribute] NVARCHAR(255) NOT NULL, 
    [Level1Description] NVARCHAR(MAX) NOT NULL, 
    [Level2Description] NVARCHAR(MAX) NOT NULL,
    [Level3Description] NVARCHAR(MAX) NOT NULL,
    [Level4Description] NVARCHAR(MAX) NOT NULL,
    [Level5Description] NVARCHAR(MAX) NOT NULL,
    [Level6Description] NVARCHAR(MAX) NOT NULL
)
