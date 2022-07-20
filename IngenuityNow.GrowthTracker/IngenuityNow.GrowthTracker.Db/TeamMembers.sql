CREATE TABLE [dbo].[TeamMembers]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [TeamId] INT NOT NULL, 
    [RoleId] INT NOT NULL 
)
