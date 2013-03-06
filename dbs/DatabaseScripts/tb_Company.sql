USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[Company]    Script Date: 02/28/2013 21:21:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
DROP TABLE [dbo].[Company]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[Company]    Script Date: 02/28/2013 21:21:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Company](
	[AdministrativeUnitID] [int] NOT NULL,
	[AccountNumber] [nvarchar](150) NULL,
	[EDRPOU] [nvarchar](150) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[AdministrativeUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

