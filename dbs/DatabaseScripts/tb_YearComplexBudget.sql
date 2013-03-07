USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[YearComplexBudget]    Script Date: 03/06/2013 22:59:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YearComplexBudget]') AND type in (N'U'))
DROP TABLE [dbo].[YearComplexBudget]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[YearComplexBudget]    Script Date: 03/06/2013 22:59:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[YearComplexBudget](
	[ComplexBudgetID] [int] NOT NULL,
	[Year] [int] NOT NULL,
 CONSTRAINT [PK_YearComplexBudget] PRIMARY KEY CLUSTERED 
(
	[ComplexBudgetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

