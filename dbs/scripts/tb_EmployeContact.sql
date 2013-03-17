USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[EmployeContact]    Script Date: 03/16/2013 19:26:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeContact]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeContact]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[EmployeContact]    Script Date: 03/16/2013 19:26:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeContact](
	[EmployeID] [int] NOT NULL,
	[Email] [nvarchar](150) NULL,
	[WorkPhone] [nvarchar](150) NULL,
	[MobilePhone] [nvarchar](150) NULL,
	[Skype] [nvarchar](150) NULL,
	[Adress] [nvarchar](150) NULL,
 CONSTRAINT [PK_EmployeContact] PRIMARY KEY CLUSTERED 
(
	[EmployeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

