CREATE TABLE [dbo].[People]
(
	[PersonId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Birthday] DATETIME NOT NULL, 
    [Email] VARCHAR(255) NOT NULL, 
    [Phone] VARCHAR(20) NOT NULL, 
    [LastModified] DATETIME NULL
)

GO

CREATE TRIGGER [dbo].[Trigger_People_LastModified]
    ON [dbo].[People]
    FOR INSERT, UPDATE
    AS
    BEGIN
        UPDATE [dbo].[People]
		SET [LastModified] = GETUTCDATE()
		FROM [dbo].[People] p 
		JOIN [inserted] i ON p.[PersonId] = i.[PersonId]
    END