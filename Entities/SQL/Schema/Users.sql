BEGIN TRANSACTION
GO
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
	CREATE TABLE dbo.Users
	(
		Id int NOT NULL IDENTITY (1, 1),
		CONSTRAINT PK_Users PRIMARY KEY CLUSTERED 
		(
			Id ASC
		)WITH( 
			STATISTICS_NORECOMPUTE = OFF, 
			IGNORE_DUP_KEY = OFF, 
			ALLOW_ROW_LOCKS = ON, 
			ALLOW_PAGE_LOCKS = ON
			) ON [PRIMARY]
	)
END
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
IF COL_LENGTH('[dbo].[Users]', 'Username') IS NULL 
	ALTER TABLE dbo.Users ADD Username nvarchar(50) NULL    
IF COL_LENGTH('[dbo].[Users]', 'FirstName') IS NULL 
	ALTER TABLE dbo.Users ADD FirstName nvarchar(50) NULL    
IF COL_LENGTH('[dbo].[Users]', 'LastName') IS NULL 
	ALTER TABLE dbo.Users ADD LastName nvarchar(50) NULL    
IF COL_LENGTH('[dbo].[Users]', 'Password') IS NULL 
	ALTER TABLE dbo.Users ADD [Password] nvarchar(50) NULL    
GO
ALTER TABLE dbo.users SET (LOCK_ESCALATION = TABLE)
COMMIT


