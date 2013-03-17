USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[MonthComplexBudget]    Script Date: 03/16/2013 15:23:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MonthComplexBudget]') AND type in (N'U'))
DROP TABLE [dbo].[MonthComplexBudget]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[MonthComplexBudget]    Script Date: 03/16/2013 15:23:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MonthComplexBudget](
	[ComplexBudgetID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
 CONSTRAINT [PK_MonthComplexBudget] PRIMARY KEY CLUSTERED 
(
	[ComplexBudgetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

