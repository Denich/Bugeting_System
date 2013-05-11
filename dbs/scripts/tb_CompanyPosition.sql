USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[CompanyPosition]    Script Date: 05/10/2013 21:48:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyPosition]') AND type in (N'U'))
DROP TABLE [dbo].[CompanyPosition]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[CompanyPosition]    Script Date: 05/10/2013 21:48:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CompanyPosition](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CompanyPosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


