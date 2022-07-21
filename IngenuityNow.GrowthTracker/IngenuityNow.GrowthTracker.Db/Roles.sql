CREATE TABLE [dbo].[Roles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [RoleGroup] NVARCHAR(255) NOT NULL, 
    [Level1Title] NVARCHAR(50) NULL,
    [Level2Title] NVARCHAR(50) NULL,
    [Level3Title] NVARCHAR(50) NULL,
    [Level4Title] NVARCHAR(50) NULL,
    [Level5Title] NVARCHAR(50) NULL,
    [Level6Title] NVARCHAR(50) NULL,
)
