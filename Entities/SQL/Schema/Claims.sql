BEGIN TRANSACTION
GO
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Claims]') AND type in (N'U'))
BEGIN
	CREATE TABLE dbo.Claims
	(
		Id int NOT NULL IDENTITY (1, 1),
		CONSTRAINT PK_Claims PRIMARY KEY CLUSTERED 
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
ALTER TABLE dbo.Claims SET (LOCK_ESCALATION = TABLE)
GO
IF COL_LENGTH('[dbo].[Claims]', 'UserId') IS NULL 
	ALTER TABLE dbo.Claims ADD UserId int  NOT NULL    
IF COL_LENGTH('[dbo].[Claims]', 'ClaimType') IS NULL 
	ALTER TABLE dbo.Claims ADD ClaimType nvarchar(50) NULL    
IF COL_LENGTH('[dbo].[Claims]', 'ClaimValue') IS NULL 
	ALTER TABLE dbo.Claims ADD ClaimValue nvarchar(50) NULL    

IF OBJECT_ID('dbo.[FK_Claims_Users]') IS NULL 
	ALTER TABLE [dbo].[Claims]  
	WITH CHECK ADD  CONSTRAINT [FK_Claims_Users] FOREIGN KEY([UserId])
	REFERENCES [dbo].[Users] ([Id])
		ON UPDATE CASCADE
		ON DELETE CASCADE
GO
ALTER TABLE dbo.Claims SET (LOCK_ESCALATION = TABLE)
COMMIT


