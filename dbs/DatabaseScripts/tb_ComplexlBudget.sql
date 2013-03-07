USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[ComplexlBudget]    Script Date: 03/06/2013 22:57:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComplexlBudget]') AND type in (N'U'))
DROP TABLE [dbo].[ComplexlBudget]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[ComplexlBudget]    Script Date: 03/06/2013 22:57:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ComplexlBudget](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[AdministrativeUnitID] [int] NOT NULL,
 CONSTRAINT [PK_ComplexlBudget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

