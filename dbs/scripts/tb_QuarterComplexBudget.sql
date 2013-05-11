USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[QuarterComplexBudget]    Script Date: 03/16/2013 15:22:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuarterComplexBudget]') AND type in (N'U'))
DROP TABLE [dbo].[QuarterComplexBudget]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[QuarterComplexBudget]    Script Date: 03/16/2013 15:23:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QuarterComplexBudget](
	[ComplexBudgetID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[QuarterNumber] [int] NOT NULL,
	[YearBudgetID] [int] NULL
 CONSTRAINT [PK_QuarterComplexBudget] PRIMARY KEY CLUSTERED 
(
	[ComplexBudgetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

