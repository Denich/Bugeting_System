USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[Employe]    Script Date: 03/16/2013 19:22:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employe]') AND type in (N'U'))
DROP TABLE [dbo].[Employe]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[Employe]    Script Date: 03/16/2013 19:22:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employe](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[SecondName] [nvarchar](150) NOT NULL,
	[MiddleName] [nvarchar](150) NULL,
	[Position] [nvarchar](150) NULL,
 CONSTRAINT [PK_Employe] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

