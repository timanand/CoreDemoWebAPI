CREATE DATABASE CoreDemoADONet
GO

USE CoreDemoADONet
CREATE TABLE [dbo].[StaffMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](max) NULL)

GO
